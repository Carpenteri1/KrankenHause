namespace KrankenHause.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class start : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InLines",
                c => new
                    {
                        InlineId = c.Int(nullable: false, identity: true),
                        LastFourDigits = c.Int(nullable: false),
                        Age = c.Int(nullable: false),
                        SocialSecurityNum = c.DateTime(nullable: false, storeType: "date"),
                        SymtomsLevel = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.InlineId);
            
            CreateTable(
                "dbo.IVAs",
                c => new
                    {
                        IVAId = c.Int(nullable: false, identity: true),
                        LastFourDigits = c.Int(nullable: false),
                        Age = c.Int(nullable: false),
                        SocialSecurityNum = c.DateTime(nullable: false, storeType: "date"),
                        SymtomsLevel = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.IVAId);
            
            CreateTable(
                "dbo.Sanatoriums",
                c => new
                    {
                        SanatoriumId = c.Int(nullable: false, identity: true),
                        LastFourDigits = c.Int(nullable: false),
                        Age = c.Int(nullable: false),
                        SocialSecurityNum = c.DateTime(nullable: false, storeType: "date"),
                        SymtomsLevel = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.SanatoriumId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sanatoriums");
            DropTable("dbo.IVAs");
            DropTable("dbo.InLines");
        }
    }
}
