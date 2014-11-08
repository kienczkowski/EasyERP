namespace EasyERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig23 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Companies", "CompanyCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Companies", "CompanyCode", c => c.String());
        }
    }
}
