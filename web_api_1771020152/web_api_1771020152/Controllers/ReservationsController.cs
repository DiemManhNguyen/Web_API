using System;
using System.Web.Http;
using web_api_1771020152.Data;
using web_api_1771020152.Models;
using web_api_1771020152.Filters;
using System.Linq;

namespace web_api_1771020152.Controllers
{
    [RoutePrefix("api/reservations")]
    [JwtAuthentication] // Toàn bộ Controller này yêu cầu đăng nhập
    public class ReservationsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // POST: api/reservations (Tạo đơn đặt bàn)
        [HttpPost]
        [Route("")]
        [Authorize]
        public IHttpActionResult CreateReservation([FromBody] Reservation reservation)
        {
            // Lấy CustomerId từ Token
            var identity = User.Identity as System.Security.Claims.ClaimsIdentity;
            var userIdStr = identity?.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;

            if (userIdStr == null) return Unauthorized();

            reservation.CustomerId = int.Parse(userIdStr);
            reservation.Status = "pending";
            reservation.PaymentStatus = "pending";
            reservation.CreatedAt = DateTime.UtcNow;

            // Sinh mã tự động: RES-YYYYMMDD-Random
            var dateStr = DateTime.Now.ToString("yyyyMMdd");
            var random = new Random().Next(1000, 9999);
            reservation.ReservationNumber = $"RES-{dateStr}-{random}";

            db.Reservations.Add(reservation);
            db.SaveChanges();

            return Ok(new { success = true, data = reservation });
        }

        // GET: api/reservations/me (Lấy lịch sử đặt bàn của mình)
        [HttpGet]
        [Route("me")]
        [Authorize]
        public IHttpActionResult GetMyReservations()
        {
            var identity = User.Identity as System.Security.Claims.ClaimsIdentity;
            var userId = int.Parse(identity.FindFirst(System.Security.Claims.ClaimTypes.Name).Value);

            var list = db.Reservations.Include("ReservationItems") // Load kèm món ăn
                                      .Where(r => r.CustomerId == userId)
                                      .OrderByDescending(r => r.CreatedAt)
                                      .ToList();

            return Ok(new { success = true, data = list });
        }
    }
}