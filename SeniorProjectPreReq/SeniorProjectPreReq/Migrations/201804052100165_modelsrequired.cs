namespace SeniorProjectPreReq.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelsrequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SchoolPdatas", "SchoolName", c => c.String(nullable: false));
            AlterColumn("dbo.SchoolPdatas", "SchoolPhone", c => c.String(nullable: false));
            AlterColumn("dbo.SchoolPdatas", "SchoolAddress", c => c.String(nullable: false));
            AlterColumn("dbo.SchoolPdatas", "SchoolWebsite", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SchoolPdatas", "SchoolWebsite", c => c.String());
            AlterColumn("dbo.SchoolPdatas", "SchoolAddress", c => c.String());
            AlterColumn("dbo.SchoolPdatas", "SchoolPhone", c => c.String());
            AlterColumn("dbo.SchoolPdatas", "SchoolName", c => c.String());
        }
    }
}
