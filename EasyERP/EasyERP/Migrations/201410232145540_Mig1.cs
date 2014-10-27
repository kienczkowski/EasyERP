namespace EasyERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        Company = c.String(nullable: false),
                        CompanyCode = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        Regon = c.String(),
                        Nip = c.String(),
                        LastName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Region = c.String(nullable: false),
                        CountryCode = c.String(nullable: false),
                        Country = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        EmailAddress = c.String(nullable: false),
                        WebSiteAddress = c.String(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ClientId);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        EnteredOn = c.DateTime(),
                        EnteredBy = c.String(),
                        Client_ClientId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clients", t => t.Client_ClientId)
                .Index(t => t.Client_ClientId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        Seller = c.String(),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Client_ClientId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Clients", t => t.Client_ClientId)
                .Index(t => t.Client_ClientId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                        Amount = c.Int(nullable: false),
                        ListPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PurchasePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QuantityPackage = c.Int(nullable: false),
                        Category = c.Int(nullable: false),
                        DeliveryDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.LogErrors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        StackTrace = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        RememberMe = c.Boolean(nullable: false),
                        EnteredOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.OrdersProducts",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderId, t.ProductId })
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrdersProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrdersProducts", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "Client_ClientId", "dbo.Clients");
            DropForeignKey("dbo.Notes", "Client_ClientId", "dbo.Clients");
            DropIndex("dbo.OrdersProducts", new[] { "ProductId" });
            DropIndex("dbo.OrdersProducts", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "Client_ClientId" });
            DropIndex("dbo.Notes", new[] { "Client_ClientId" });
            DropTable("dbo.OrdersProducts");
            DropTable("dbo.Users");
            DropTable("dbo.LogErrors");
            DropTable("dbo.Products");
            DropTable("dbo.Orders");
            DropTable("dbo.Notes");
            DropTable("dbo.Clients");
        }
    }
}
