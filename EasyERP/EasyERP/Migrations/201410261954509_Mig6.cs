namespace EasyERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        NameCompany = c.String(),
                        CompanyCode = c.String(),
                        Regon = c.String(),
                        Nip = c.String(),
                        Address = c.String(),
                        Region = c.String(),
                        Country = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        FaxNumber = c.String(),
                        WebSiteAddress = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CompanyId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Companies");
        }
    }
}
