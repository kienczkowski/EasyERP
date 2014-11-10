namespace EasyERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Templates", "TemplateFile", c => c.Binary());
            DropColumn("dbo.Templates", "Data");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Templates", "Data", c => c.Binary());
            DropColumn("dbo.Templates", "TemplateFile");
        }
    }
}
