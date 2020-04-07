namespace KrankenHause.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeAgeNullible : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AfterLives", "Age", c => c.Int());
            AlterColumn("dbo.InLines", "Age", c => c.Int());
            AlterColumn("dbo.IVAs", "Age", c => c.Int());
            AlterColumn("dbo.Recovereds", "Age", c => c.Int());
            AlterColumn("dbo.Sanatoriums", "Age", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sanatoriums", "Age", c => c.Int(nullable: false));
            AlterColumn("dbo.Recovereds", "Age", c => c.Int(nullable: false));
            AlterColumn("dbo.IVAs", "Age", c => c.Int(nullable: false));
            AlterColumn("dbo.InLines", "Age", c => c.Int(nullable: false));
            AlterColumn("dbo.AfterLives", "Age", c => c.Int(nullable: false));
        }
    }
}
