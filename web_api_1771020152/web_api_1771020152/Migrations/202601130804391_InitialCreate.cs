namespace web_api_1771020152.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MenuItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ReservationItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReservationId = c.Int(nullable: false),
                        MenuItemId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MenuItems", t => t.MenuItemId, cascadeDelete: true)
                .ForeignKey("dbo.Reservations", t => t.ReservationId, cascadeDelete: true)
                .Index(t => t.ReservationId)
                .Index(t => t.MenuItemId);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        TableId = c.Int(nullable: false),
                        ReservationDate = c.DateTime(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Tables", t => t.TableId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.TableId);
            
            CreateTable(
                "dbo.Tables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TableNumber = c.String(),
                        Capacity = c.Int(nullable: false),
                        IsAvailable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReservationItems", "ReservationId", "dbo.Reservations");
            DropForeignKey("dbo.Reservations", "TableId", "dbo.Tables");
            DropForeignKey("dbo.Reservations", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.ReservationItems", "MenuItemId", "dbo.MenuItems");
            DropIndex("dbo.Reservations", new[] { "TableId" });
            DropIndex("dbo.Reservations", new[] { "CustomerId" });
            DropIndex("dbo.ReservationItems", new[] { "MenuItemId" });
            DropIndex("dbo.ReservationItems", new[] { "ReservationId" });
            DropTable("dbo.Tables");
            DropTable("dbo.Reservations");
            DropTable("dbo.ReservationItems");
            DropTable("dbo.MenuItems");
            DropTable("dbo.Customers");
        }
    }
}
