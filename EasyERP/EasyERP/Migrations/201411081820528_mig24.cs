namespace EasyERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig24 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clients", "CompanyCode", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clients", "CompanyCode", c => c.String(nullable: false));
        }
    }
}
