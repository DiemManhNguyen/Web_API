namespace web_api_1771020152.Migrations // CHÚ Ý NAMESPACE
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using web_api_1771020152.Models; // CHÚ Ý NAMESPACE

    internal sealed class Configuration : DbMigrationsConfiguration<web_api_1771020152.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(web_api_1771020152.Data.ApplicationDbContext context)
        {
            // 1. Tạo Customer mẫu (Mật khẩu nên mã hóa trong thực tế, bài thi có thể để text)
            context.Customers.AddOrUpdate(x => x.Email,
                new Customer
                {
                    Email = "admin@example.com",
                    Password = "123", // Password đơn giản để test
                    FullName = "Admin User",
                    PhoneNumber = "0909000111",
                    Address = "Hanoi",
                    LoyaltyPoints = 100,
                    IsActive = true
                },
                new Customer
                {
                    Email = "khach1@example.com",
                    Password = "123",
                    FullName = "Nguyen Van A",
                    PhoneNumber = "0909000222",
                    LoyaltyPoints = 0
                }
            );

            // 2. Tạo MenuItems mẫu (Món ăn)
            context.MenuItems.AddOrUpdate(x => x.Name,
                new MenuItem
                {
                    Name = "Phở Bò",
                    Category = "Main Course",
                    Price = 50000,
                    Description = "Phở bò tái chín",
                    PreparationTime = 15,
                    IsAvailable = true
                },
                new MenuItem
                {
                    Name = "Trà Đá",
                    Category = "Beverage",
                    Price = 5000,
                    Description = "Trà đá vỉa hè",
                    PreparationTime = 2,
                    IsAvailable = true
                },
                new MenuItem
                {
                    Name = "Gỏi Cuốn",
                    Category = "Appetizer",
                    Price = 30000,
                    IsVegetarian = false,
                    IsAvailable = true
                }
            );

            // 3. Tạo Tables mẫu (Bàn ăn)
            context.Tables.AddOrUpdate(x => x.TableNumber,
                new Table { TableNumber = "T01", Capacity = 4, IsAvailable = true },
                new Table { TableNumber = "T02", Capacity = 2, IsAvailable = true },
                new Table { TableNumber = "VIP", Capacity = 10, IsAvailable = true }
            );

            // Lưu thay đổi
            context.SaveChanges();
        }
    }
}