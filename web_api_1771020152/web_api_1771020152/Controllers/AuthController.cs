using System;
using System.Linq;
using System.Web.Http;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using web_api_1771020152.Data;   // Namespace Data của bạn
using web_api_1771020152.Models; // Namespace Models/DTOs của bạn

namespace web_api_1771020152.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Key bí mật để ký Token (Nên dài ít nhất 32 ký tự)
        private const string SECRET_KEY = "day-la-khoa-bi-mat-cua-sinh-vien-1771020152-rat-dai-va-bao-mat";

        // 1. API ĐĂNG KÝ
        [HttpPost]
        [Route("register")]
        public IHttpActionResult Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Kiểm tra email đã tồn tại chưa
            if (db.Customers.Any(c => c.Email == registerDto.Email))
            {
                return BadRequest("Email already exists");
            }

            // Tạo Customer mới
            var customer = new Customer
            {
                Email = registerDto.Email,
                // Hash password trước khi lưu (Hàm Hash ở cuối file)
                Password = HashPassword(registerDto.Password),
                FullName = registerDto.FullName,
                PhoneNumber = registerDto.PhoneNumber,
                Address = registerDto.Address,
                LoyaltyPoints = 0,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            db.Customers.Add(customer);
            db.SaveChanges();

            return Ok(new { success = true, message = "Đăng ký thành công" });
        }

        // 2. API ĐĂNG NHẬP (QUAN TRỌNG NHẤT)
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var hashedPassword = HashPassword(loginDto.Password);

            // Tìm user trong DB
            var customer = db.Customers.FirstOrDefault(c =>
                c.Email == loginDto.Email &&
                c.Password == hashedPassword &&
                c.IsActive);

            if (customer == null)
            {
                return Content(System.Net.HttpStatusCode.Unauthorized, new { success = false, message = "Sai email hoặc mật khẩu" });
            }

            // Tạo Token
            var token = GenerateJwtToken(customer);

            // Trả về kết quả đúng format đề thi yêu cầu
            return Ok(new LoginResponseDto
            {
                Success = true,
                Token = token,
                User = new
                {
                    customer.Id,
                    customer.Email,
                    customer.FullName,
                    customer.LoyaltyPoints
                },
                // BẮT BUỘC: Hardcode Mã Sinh Viên của bạn
                StudentId = "1771020152"
            });
        }

        // 3. API LẤY THÔNG TIN USER HIỆN TẠI (GET /me)
        [HttpGet]
        [Route("me")]
        [Authorize] // Yêu cầu phải có Token mới gọi được
        public IHttpActionResult GetCurrentUser()
        {
            // Lấy ID từ Token (User.Identity.Name được set bởi JWT Middleware)
            // Lưu ý: Cần cấu hình Middleware xác thực JWT ở bước sau mới chạy được dòng này
            if (User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(User.Identity.Name);
                var customer = db.Customers.Find(userId);

                if (customer == null) return NotFound();

                return Ok(new { success = true, data = customer });
            }
            return Unauthorized();
        }

        // --- CÁC HÀM HỖ TRỢ (HELPER) ---

        // Hàm băm mật khẩu đơn giản (SHA256)
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        // Hàm tạo JWT Token
        private string GenerateJwtToken(Customer customer)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, customer.Id.ToString()), // Lưu ID vào claim Name
                new Claim(ClaimTypes.Email, customer.Email),
                new Claim("FullName", customer.FullName)
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7), // Token hết hạn sau 7 ngày
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}