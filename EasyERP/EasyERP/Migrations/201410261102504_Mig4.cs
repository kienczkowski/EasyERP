namespace EasyERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tasks", "Client_ClientId", "dbo.Clients");
            DropIndex("dbo.Tasks", new[] { "Client_ClientId" });
            AddColumn("dbo.Tasks", "User_UserId", c => c.Int());
            CreateIndex("dbo.Tasks", "User_UserId");
            AddForeignKey("dbo.Tasks", "User_UserId", "dbo.Users", "UserId");
            DropColumn("dbo.Tasks", "Client_ClientId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tasks", "Client_ClientId", c => c.Int());
            DropForeignKey("dbo.Tasks", "User_UserId", "dbo.Users");
            DropIndex("dbo.Tasks", new[] { "User_UserId" });
            DropColumn("dbo.Tasks", "User_UserId");
            CreateIndex("dbo.Tasks", "Client_ClientId");
            AddForeignKey("dbo.Tasks", "Client_ClientId", "dbo.Clients", "ClientId");
        }
    }
}
