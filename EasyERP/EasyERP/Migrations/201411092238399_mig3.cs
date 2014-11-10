namespace EasyERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Templates",
                c => new
                    {
                        TemplateId = c.Int(nullable: false, identity: true),
                        TemplateName = c.String(nullable: false),
                        Data = c.Binary(),
                        EnteredOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TemplateId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Templates");
        }
    }
}
