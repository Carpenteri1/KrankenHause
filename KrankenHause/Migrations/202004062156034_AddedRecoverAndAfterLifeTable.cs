namespace KrankenHause.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRecoverAndAfterLifeTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AfterLives",
                c => new
                    {
                        AfterLifeId = c.Int(nullable: false, identity: true),
                        LastFourDigits = c.Int(nullable: false),
                        Age = c.Int(nullable: false),
                        SocialSecurityNum = c.DateTime(nullable: false, storeType: "date"),
                        SymtomsLevel = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.AfterLifeId);
            
            CreateTable(
                "dbo.Recovereds",
                c => new
                    {
                        RecoverdId = c.Int(nullable: false, identity: true),
                        LastFourDigits = c.Int(nullable: false),
                        Age = c.Int(nullable: false),
                        SocialSecurityNum = c.DateTime(nullable: false, storeType: "date"),
                        SymtomsLevel = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.RecoverdId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Recovereds");
            DropTable("dbo.AfterLives");
        }
    }
}
