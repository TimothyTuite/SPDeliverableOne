namespace SeniorProjectPreReq.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDatabase : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Schools", "SchoolGrade");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Schools", "SchoolGrade", c => c.String());
        }
    }
}
