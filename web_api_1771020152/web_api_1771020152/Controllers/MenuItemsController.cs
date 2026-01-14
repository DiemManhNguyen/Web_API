using System.Linq;
using System.Web.Http;
using web_api_1771020152.Data;
using web_api_1771020152.Models;
using web_api_1771020152.Filters; // Để dùng [JwtAuthentication]

namespace web_api_1771020152.Controllers
{
    [RoutePrefix("api/menu-items")]
    public class MenuItemsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/menu-items (Có phân trang & tìm kiếm)
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetMenuItems(
            int page = 1,
            int limit = 10,
            string search = null,
            string category = null,
            bool? isVegetarian = null)
        {
            var query = db.MenuItems.AsQueryable();

            // Lọc theo tên
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(m => m.Name.Contains(search) || m.Description.Contains(search));
            }

            // Lọc theo danh mục
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(m => m.Category == category);
            }

            // Lọc món chay
            if (isVegetarian.HasValue)
            {
                query = query.Where(m => m.IsVegetarian == isVegetarian.Value);
            }

            // Phân trang
            int total = query.Count();
            var items = query.OrderBy(m => m.Name)
                             .Skip((page - 1) * limit)
                             .Take(limit)
                             .ToList();

            return Ok(new
            {
                success = true,
                data = items,
                pagination = new { page, limit, total }
            });
        }

        // POST: api/menu-items (Chỉ Admin mới được thêm - Cần Token)
        [HttpPost]
        [Route("")]
        [JwtAuthentication] // Yêu cầu đăng nhập
        [Authorize]         // Yêu cầu token hợp lệ
        public IHttpActionResult CreateMenuItem([FromBody] MenuItem menuItem)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            db.MenuItems.Add(menuItem);
            db.SaveChanges();

            return Ok(new { success = true, data = menuItem });
        }
    }
}