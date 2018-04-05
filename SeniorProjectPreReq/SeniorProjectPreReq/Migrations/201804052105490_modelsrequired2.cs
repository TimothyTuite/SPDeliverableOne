namespace SeniorProjectPreReq.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelsrequired2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SchoolPdatas", "SchoolPrincipal", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SchoolPdatas", "SchoolPrincipal", c => c.String());
        }
    }
}
