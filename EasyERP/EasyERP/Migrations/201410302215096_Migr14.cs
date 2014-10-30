namespace EasyERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migr14 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Seller", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "StartDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "StartDate", c => c.DateTime());
            AlterColumn("dbo.Orders", "Seller", c => c.String());
        }
    }
}
