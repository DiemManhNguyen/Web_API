using System.Data.Entity;
using web_api_1771020152.Models;

namespace web_api_1771020152.Data // LƯU Ý: Đổi namespace tương ứng
{
    public class ApplicationDbContext : DbContext
    {
        // "DefaultConnection" phải trùng với tên trong Web.config
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationItem> ReservationItems { get; set; }
        public DbSet<Table> Tables { get; set; }

        // Cấu hình Fluent API để thiết lập mối quan hệ và ràng buộc
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Cấu hình quan hệ Reservation - Customer
            // Yêu cầu: ON DELETE RESTRICT (Xóa Customer không được xóa Reservation cũ)
            modelBuilder.Entity<Reservation>()
                .HasRequired(r => r.Customer)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.CustomerId)
                .WillCascadeOnDelete(false); // [cite: 1003, 2255]

            // 2. Cấu hình quan hệ ReservationItem - Reservation
            // Yêu cầu: ON DELETE CASCADE (Xóa Reservation thì xóa luôn các món đã đặt)
            modelBuilder.Entity<ReservationItem>()
                .HasRequired(ri => ri.Reservation)
                .WithMany(r => r.ReservationItems)
                .HasForeignKey(ri => ri.ReservationId)
                .WillCascadeOnDelete(true); // [cite: 1006, 2256]

            // 3. Cấu hình quan hệ ReservationItem - MenuItem
            // Yêu cầu: ON DELETE RESTRICT (Xóa món ăn trong menu không được mất lịch sử đặt món)
            modelBuilder.Entity<ReservationItem>()
                .HasRequired(ri => ri.MenuItem)
                .WithMany() // MenuItem không cần giữ list ReservationItems cũng được
                .HasForeignKey(ri => ri.MenuItemId)
                .WillCascadeOnDelete(false); // [cite: 1016, 2260]

            // 4. Các ràng buộc Duy nhất (Unique Constraints) theo đề bài
            // Email khách hàng không được trùng
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Email)
                .IsUnique(); // [cite: 1019, 2192]

            // Mã đặt bàn không được trùng
            modelBuilder.Entity<Reservation>()
                .HasIndex(r => r.ReservationNumber)
                .IsUnique(); // [cite: 1021, 2261]

            // Số bàn không được trùng
            modelBuilder.Entity<Table>()
                .HasIndex(t => t.TableNumber)
                .IsUnique(); // [cite: 1023, 2261]
        }
    }
}