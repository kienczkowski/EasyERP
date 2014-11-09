namespace EasyERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductOrders", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductOrders", "Order_OrderId", "dbo.Orders");
            DropIndex("dbo.ProductOrders", new[] { "Product_ProductId" });
            DropIndex("dbo.ProductOrders", new[] { "Order_OrderId" });
            DropTable("dbo.ProductOrders");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductOrders",
                c => new
                    {
                        Product_ProductId = c.Int(nullable: false),
                        Order_OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_ProductId, t.Order_OrderId });
            
            CreateIndex("dbo.ProductOrders", "Order_OrderId");
            CreateIndex("dbo.ProductOrders", "Product_ProductId");
            AddForeignKey("dbo.ProductOrders", "Order_OrderId", "dbo.Orders", "OrderId", cascadeDelete: true);
            AddForeignKey("dbo.ProductOrders", "Product_ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
        }
    }
}
