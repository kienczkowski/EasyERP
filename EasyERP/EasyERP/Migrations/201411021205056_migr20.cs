namespace EasyERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migr20 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tasks", "TaskDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Tasks", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tasks", "Description", c => c.String());
            AlterColumn("dbo.Tasks", "TaskDate", c => c.DateTime());
        }
    }
}
