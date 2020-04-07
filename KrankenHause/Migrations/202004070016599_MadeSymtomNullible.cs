namespace KrankenHause.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeSymtomNullible : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AfterLives", "SymtomsLevel", c => c.Int());
            AlterColumn("dbo.InLines", "SymtomsLevel", c => c.Int());
            AlterColumn("dbo.IVAs", "SymtomsLevel", c => c.Int());
            AlterColumn("dbo.Recovereds", "SymtomsLevel", c => c.Int());
            AlterColumn("dbo.Sanatoriums", "SymtomsLevel", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sanatoriums", "SymtomsLevel", c => c.Int(nullable: false));
            AlterColumn("dbo.Recovereds", "SymtomsLevel", c => c.Int(nullable: false));
            AlterColumn("dbo.IVAs", "SymtomsLevel", c => c.Int(nullable: false));
            AlterColumn("dbo.InLines", "SymtomsLevel", c => c.Int(nullable: false));
            AlterColumn("dbo.AfterLives", "SymtomsLevel", c => c.Int(nullable: false));
        }
    }
}
