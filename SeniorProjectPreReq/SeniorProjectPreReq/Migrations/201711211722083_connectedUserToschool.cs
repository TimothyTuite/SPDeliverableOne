namespace SeniorProjectPreReq.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class connectedUserToschool : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Schools", "Administrator_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Schools", new[] { "Administrator_Id" });
            AddColumn("dbo.AspNetUsers", "schoolID", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "schoolID");
            AddForeignKey("dbo.AspNetUsers", "schoolID", "dbo.Schools", "ID");
            DropColumn("dbo.Schools", "Administrator_Id");
            DropColumn("dbo.AspNetUsers", "school");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "school", c => c.String());
            AddColumn("dbo.Schools", "Administrator_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.AspNetUsers", "schoolID", "dbo.Schools");
            DropIndex("dbo.AspNetUsers", new[] { "schoolID" });
            DropColumn("dbo.AspNetUsers", "schoolID");
            CreateIndex("dbo.Schools", "Administrator_Id");
            AddForeignKey("dbo.Schools", "Administrator_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
