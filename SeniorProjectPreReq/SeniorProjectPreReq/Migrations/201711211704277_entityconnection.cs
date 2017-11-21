namespace SeniorProjectPreReq.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class entityconnection : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Programs",
                c => new
                    {
                        ProgramID = c.String(nullable: false, maxLength: 128),
                        Program_Name = c.String(),
                        Program_Description = c.String(),
                        Program_Level = c.String(),
                    })
                .PrimaryKey(t => t.ProgramID);
            
            AddColumn("dbo.Schools", "Administrator_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Pictures", "School_id", c => c.String(maxLength: 128));
            AlterColumn("dbo.SchoolsPrograms", "SchoolID", c => c.String(maxLength: 128));
            AlterColumn("dbo.SchoolsPrograms", "ProgramID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Pictures", "School_id");
            CreateIndex("dbo.Schools", "Administrator_Id");
            CreateIndex("dbo.SchoolsPrograms", "SchoolID");
            CreateIndex("dbo.SchoolsPrograms", "ProgramID");
            AddForeignKey("dbo.Schools", "Administrator_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.SchoolsPrograms", "ProgramID", "dbo.Programs", "ProgramID");
            AddForeignKey("dbo.SchoolsPrograms", "SchoolID", "dbo.Schools", "ID");
            AddForeignKey("dbo.Pictures", "School_id", "dbo.Schools", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pictures", "School_id", "dbo.Schools");
            DropForeignKey("dbo.SchoolsPrograms", "SchoolID", "dbo.Schools");
            DropForeignKey("dbo.SchoolsPrograms", "ProgramID", "dbo.Programs");
            DropForeignKey("dbo.Schools", "Administrator_Id", "dbo.AspNetUsers");
            DropIndex("dbo.SchoolsPrograms", new[] { "ProgramID" });
            DropIndex("dbo.SchoolsPrograms", new[] { "SchoolID" });
            DropIndex("dbo.Schools", new[] { "Administrator_Id" });
            DropIndex("dbo.Pictures", new[] { "School_id" });
            AlterColumn("dbo.SchoolsPrograms", "ProgramID", c => c.String());
            AlterColumn("dbo.SchoolsPrograms", "SchoolID", c => c.String());
            AlterColumn("dbo.Pictures", "School_id", c => c.String());
            DropColumn("dbo.Schools", "Administrator_Id");
            DropTable("dbo.Programs");
        }
    }
}
