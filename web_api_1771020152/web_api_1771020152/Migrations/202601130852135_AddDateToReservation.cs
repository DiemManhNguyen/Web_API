namespace web_api_1771020152.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateToReservation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "TableId", "dbo.Tables");
            DropForeignKey("dbo.ReservationItems", "MenuItemId", "dbo.MenuItems");
            DropForeignKey("dbo.Reservations", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Reservations", new[] { "TableId" });
            AddColumn("dbo.Customers", "Password", c => c.String(nullable: false));
            AddColumn("dbo.Customers", "FullName", c => c.String(nullable: false));
            AddColumn("dbo.Customers", "PhoneNumber", c => c.String());
            AddColumn("dbo.Customers", "Address", c => c.String());
            AddColumn("dbo.Customers", "LoyaltyPoints", c => c.Int(nullable: false));
            AddColumn("dbo.Customers", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Customers", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.MenuItems", "ImageUrl", c => c.String());
            AddColumn("dbo.MenuItems", "PreparationTime", c => c.Int(nullable: false));
            AddColumn("dbo.MenuItems", "IsVegetarian", c => c.Boolean(nullable: false));
            AddColumn("dbo.MenuItems", "IsSpicy", c => c.Boolean(nullable: false));
            AddColumn("dbo.MenuItems", "IsAvailable", c => c.Boolean(nullable: false));
            AddColumn("dbo.MenuItems", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.ReservationItems", "Price", c => c.Decimal(nullable: false, storeType: "money"));
            AddColumn("dbo.ReservationItems", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Reservations", "ReservationNumber", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AddColumn("dbo.Reservations", "NumberOfGuests", c => c.Int(nullable: false));
            AddColumn("dbo.Reservations", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Reservations", "PaymentStatus", c => c.String());
            AddColumn("dbo.Reservations", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Reservations", "UpdatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tables", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tables", "UpdatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Customers", "Email", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.MenuItems", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Tables", "TableNumber", c => c.String(nullable: false, maxLength: 20));
            CreateIndex("dbo.Customers", "Email", unique: true);
            CreateIndex("dbo.Reservations", "ReservationNumber", unique: true);
            CreateIndex("dbo.Tables", "TableNumber", unique: true);
            AddForeignKey("dbo.ReservationItems", "MenuItemId", "dbo.MenuItems", "Id");
            AddForeignKey("dbo.Reservations", "CustomerId", "dbo.Customers", "Id");
            DropColumn("dbo.Customers", "Name");
            DropColumn("dbo.Customers", "Phone");
            DropColumn("dbo.Reservations", "TableId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservations", "TableId", c => c.Int(nullable: false));
            AddColumn("dbo.Customers", "Phone", c => c.String());
            AddColumn("dbo.Customers", "Name", c => c.String());
            DropForeignKey("dbo.Reservations", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.ReservationItems", "MenuItemId", "dbo.MenuItems");
            DropIndex("dbo.Tables", new[] { "TableNumber" });
            DropIndex("dbo.Reservations", new[] { "ReservationNumber" });
            DropIndex("dbo.Customers", new[] { "Email" });
            AlterColumn("dbo.Tables", "TableNumber", c => c.String());
            AlterColumn("dbo.MenuItems", "Name", c => c.String());
            AlterColumn("dbo.Customers", "Email", c => c.String());
            DropColumn("dbo.Tables", "UpdatedAt");
            DropColumn("dbo.Tables", "CreatedAt");
            DropColumn("dbo.Reservations", "UpdatedAt");
            DropColumn("dbo.Reservations", "CreatedAt");
            DropColumn("dbo.Reservations", "PaymentStatus");
            DropColumn("dbo.Reservations", "Total");
            DropColumn("dbo.Reservations", "NumberOfGuests");
            DropColumn("dbo.Reservations", "ReservationNumber");
            DropColumn("dbo.ReservationItems", "CreatedAt");
            DropColumn("dbo.ReservationItems", "Price");
            DropColumn("dbo.MenuItems", "CreatedAt");
            DropColumn("dbo.MenuItems", "IsAvailable");
            DropColumn("dbo.MenuItems", "IsSpicy");
            DropColumn("dbo.MenuItems", "IsVegetarian");
            DropColumn("dbo.MenuItems", "PreparationTime");
            DropColumn("dbo.MenuItems", "ImageUrl");
            DropColumn("dbo.Customers", "CreatedAt");
            DropColumn("dbo.Customers", "IsActive");
            DropColumn("dbo.Customers", "LoyaltyPoints");
            DropColumn("dbo.Customers", "Address");
            DropColumn("dbo.Customers", "PhoneNumber");
            DropColumn("dbo.Customers", "FullName");
            DropColumn("dbo.Customers", "Password");
            CreateIndex("dbo.Reservations", "TableId");
            AddForeignKey("dbo.Reservations", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ReservationItems", "MenuItemId", "dbo.MenuItems", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Reservations", "TableId", "dbo.Tables", "Id", cascadeDelete: true);
        }
    }
}
