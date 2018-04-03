namespace SeniorProjectPreReq.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class metric : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Metrics", "value", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Metrics", "value");
        }
    }
}
