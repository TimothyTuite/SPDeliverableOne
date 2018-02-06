namespace SeniorProjectPreReq.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _base : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.highschoolProfiles",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        schoolID = c.Int(nullable: false),
                        dateCreated = c.DateTime(nullable: false),
                        dateApproved = c.DateTime(nullable: false),
                        schoolGrade = c.String(),
                        enrollment = c.Int(nullable: false),
                        freeOrReduced = c.Single(nullable: false),
                        isCharter = c.Boolean(nullable: false),
                        isMagnet = c.Boolean(nullable: false),
                        gradRate = c.Single(nullable: false),
                        emLearningGains = c.Single(nullable: false),
                        accelCoursework = c.Single(nullable: false),
                        emPerformance = c.Single(nullable: false),
                        AfricanAmerican = c.Single(nullable: false),
                        White = c.Single(nullable: false),
                        Asian = c.Single(nullable: false),
                        Hispanic = c.Single(nullable: false),
                        MixedOther = c.Single(nullable: false),
                        unspecified = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Schools", t => t.schoolID, cascadeDelete: true)
                .Index(t => t.schoolID);
            
            CreateTable(
                "dbo.k_12Profile",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        schoolID = c.Int(nullable: false),
                        dateCreated = c.DateTime(nullable: false),
                        dateApproved = c.DateTime(nullable: false),
                        schoolGrade = c.String(),
                        enrollment = c.Int(nullable: false),
                        freeOrReduced = c.Single(nullable: false),
                        isCharter = c.Boolean(nullable: false),
                        isMagnet = c.Boolean(nullable: false),
                        KReadiness = c.Single(nullable: false),
                        algPassRate = c.Single(nullable: false),
                        gradRate = c.Single(nullable: false),
                        emLearningGains = c.Single(nullable: false),
                        accelCoursework = c.Single(nullable: false),
                        emPerformance = c.Single(nullable: false),
                        AfricanAmerican = c.Single(nullable: false),
                        White = c.Single(nullable: false),
                        Asian = c.Single(nullable: false),
                        Hispanic = c.Single(nullable: false),
                        MixedOther = c.Single(nullable: false),
                        unspecified = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Schools", t => t.schoolID, cascadeDelete: true)
                .Index(t => t.schoolID);
            
            AddColumn("dbo.SchoolsPrograms", "highschoolProfile_id", c => c.Int());
            AddColumn("dbo.SchoolsPrograms", "k_12Profile_id", c => c.Int());
            CreateIndex("dbo.SchoolsPrograms", "highschoolProfile_id");
            CreateIndex("dbo.SchoolsPrograms", "k_12Profile_id");
            AddForeignKey("dbo.SchoolsPrograms", "highschoolProfile_id", "dbo.highschoolProfiles", "id");
            AddForeignKey("dbo.SchoolsPrograms", "k_12Profile_id", "dbo.k_12Profile", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.k_12Profile", "schoolID", "dbo.Schools");
            DropForeignKey("dbo.SchoolsPrograms", "k_12Profile_id", "dbo.k_12Profile");
            DropForeignKey("dbo.highschoolProfiles", "schoolID", "dbo.Schools");
            DropForeignKey("dbo.SchoolsPrograms", "highschoolProfile_id", "dbo.highschoolProfiles");
            DropIndex("dbo.k_12Profile", new[] { "schoolID" });
            DropIndex("dbo.SchoolsPrograms", new[] { "k_12Profile_id" });
            DropIndex("dbo.SchoolsPrograms", new[] { "highschoolProfile_id" });
            DropIndex("dbo.highschoolProfiles", new[] { "schoolID" });
            DropColumn("dbo.SchoolsPrograms", "k_12Profile_id");
            DropColumn("dbo.SchoolsPrograms", "highschoolProfile_id");
            DropTable("dbo.k_12Profile");
            DropTable("dbo.highschoolProfiles");
        }
    }
}
