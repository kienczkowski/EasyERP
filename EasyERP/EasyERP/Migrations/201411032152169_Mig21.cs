namespace EasyERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig21 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.OrdersProducts", name: "OrderId", newName: "Order_OrderId");
            RenameColumn(table: "dbo.OrdersProducts", name: "ProductId", newName: "Product_ProductId");
            RenameIndex(table: "dbo.OrdersProducts", name: "IX_OrderId", newName: "IX_Order_OrderId");
            RenameIndex(table: "dbo.OrdersProducts", name: "IX_ProductId", newName: "IX_Product_ProductId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.OrdersProducts", name: "IX_Product_ProductId", newName: "IX_ProductId");
            RenameIndex(table: "dbo.OrdersProducts", name: "IX_Order_OrderId", newName: "IX_OrderId");
            RenameColumn(table: "dbo.OrdersProducts", name: "Product_ProductId", newName: "ProductId");
            RenameColumn(table: "dbo.OrdersProducts", name: "Order_OrderId", newName: "OrderId");
        }
    }
}
