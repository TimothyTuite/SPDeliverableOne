namespace SeniorProjectPreReq.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class metric2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SchoolMetricValues", "value", c => c.Single(nullable: false));
            DropColumn("dbo.Metrics", "value");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Metrics", "value", c => c.Single(nullable: false));
            DropColumn("dbo.SchoolMetricValues", "value");
        }
    }
}
