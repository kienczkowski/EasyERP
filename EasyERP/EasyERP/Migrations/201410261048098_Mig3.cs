namespace EasyERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        TaskId = c.Int(nullable: false, identity: true),
                        TaskType = c.Int(nullable: false),
                        Priority = c.Int(nullable: false),
                        TaskDate = c.DateTime(),
                        Description = c.String(),
                        Client_ClientId = c.Int(),
                    })
                .PrimaryKey(t => t.TaskId)
                .ForeignKey("dbo.Clients", t => t.Client_ClientId)
                .Index(t => t.Client_ClientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "Client_ClientId", "dbo.Clients");
            DropIndex("dbo.Tasks", new[] { "Client_ClientId" });
            DropTable("dbo.Tasks");
        }
    }
}
