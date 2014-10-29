namespace EasyERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig13 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Client_ClientId", "dbo.Clients");
            DropIndex("dbo.Orders", new[] { "Client_ClientId" });
            AlterColumn("dbo.Orders", "Client_ClientId", c => c.Int());
            CreateIndex("dbo.Orders", "Client_ClientId");
            AddForeignKey("dbo.Orders", "Client_ClientId", "dbo.Clients", "ClientId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Client_ClientId", "dbo.Clients");
            DropIndex("dbo.Orders", new[] { "Client_ClientId" });
            AlterColumn("dbo.Orders", "Client_ClientId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "Client_ClientId");
            AddForeignKey("dbo.Orders", "Client_ClientId", "dbo.Clients", "ClientId", cascadeDelete: true);
        }
    }
}
