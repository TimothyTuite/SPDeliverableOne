namespace SeniorProjectPreReq.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
       
            CreateTable(
                "dbo.Metrics",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MetricName = c.String(maxLength: 50),
                        Description = c.String(maxLength: 450),
                        schoolLevel = c.Int(nullable: false),
                        rangeTop = c.Single(nullable: false),
                        rangeBottom = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SchoolTypes", t => t.schoolLevel, cascadeDelete: true)
                .Index(t => t.schoolLevel);
            
            CreateTable(
                "dbo.SchoolTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Programs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        programName = c.String(maxLength: 50),
                        programDescription = c.String(maxLength: 450),
                        TypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SchoolTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.TypeID);
            
            CreateTable(
                "dbo.SchoolMetricValues",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        schoolID = c.Int(nullable: false),
                        metricID = c.Int(nullable: false),
                        Approved = c.Boolean(nullable: false),
                        year = c.Int(nullable: false),
                        dateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Metrics", t => t.metricID, cascadeDelete: true)
                .ForeignKey("dbo.SchoolPdatas", t => t.schoolID, cascadeDelete: true)
                .Index(t => t.schoolID)
                .Index(t => t.metricID);
            
            CreateTable(
                "dbo.SchoolPdatas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SchoolName = c.String(),
                        SchoolPhone = c.String(),
                        SchoolAddress = c.String(),
                        SchoolWebsite = c.String(),
                        SchoolPrincipal = c.String(),
                        schoolTypeID = c.Int(nullable: false),
                        Approved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SchoolTypes", t => t.schoolTypeID, cascadeDelete: true)
                .Index(t => t.schoolTypeID);
            
            CreateTable(
                "dbo.SchoolProgramsValues",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        schoolID = c.Int(nullable: false),
                        programID = c.Int(nullable: false),
                        year = c.Int(nullable: false),
                        Approved = c.Boolean(nullable: false),
                        hasProgram = c.Boolean(nullable: false),
                        dateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SchoolPdatas", t => t.schoolID, cascadeDelete: true)
                .ForeignKey("dbo.Programs", t => t.programID, cascadeDelete: true)
                .Index(t => t.schoolID)
                .Index(t => t.programID);
            
            CreateTable(
                "dbo.youtubeURLs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        schoolID = c.Int(nullable: false),
                        URL = c.String(),
                        year = c.Int(nullable: false),
                        Approved = c.Boolean(nullable: false),
                        dateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SchoolPdatas", t => t.schoolID, cascadeDelete: true)
                .Index(t => t.schoolID);
            
         
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Programs",
                c => new
                    {
                        ProgramID = c.Int(nullable: false, identity: true),
                        Program_Name = c.String(),
                        Program_Description = c.String(),
                        Program_Level = c.String(),
                    })
                .PrimaryKey(t => t.ProgramID);
            
            CreateTable(
                "dbo.SchoolsPrograms",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SchoolID = c.Int(nullable: false),
                        ProgramID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Schools",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SchoolName = c.String(),
                        SchoolPhone = c.String(),
                        SchoolAddress = c.String(),
                        SchoolWebsite = c.String(),
                        SchoolPrincipal = c.String(),
                        StudentsEnrolled = c.Int(nullable: false),
                        ReducedLunchPercentage = c.Int(nullable: false),
                        IsCharter = c.Boolean(nullable: false),
                        MHSReadingPercentage = c.Int(nullable: false),
                        MHSMathPercentage = c.Int(nullable: false),
                        AlgebraIPassRatePercentage = c.Int(nullable: false),
                        AccCourseParticipationPercentage = c.Int(nullable: false),
                        PSRReadyReadingPercentage = c.Int(nullable: false),
                        PSReadyMathPercentage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        School_id = c.Int(nullable: false),
                        picture = c.Binary(),
                    })
                .PrimaryKey(t => t.id);
            
            DropForeignKey("dbo.youtubeURLs", "schoolID", "dbo.SchoolPdatas");
            DropForeignKey("dbo.SchoolProgramsValues", "programID", "dbo.Programs");
            DropForeignKey("dbo.SchoolProgramsValues", "schoolID", "dbo.SchoolPdatas");
            DropForeignKey("dbo.SchoolMetricValues", "schoolID", "dbo.SchoolPdatas");
            DropForeignKey("dbo.SchoolPdatas", "schoolTypeID", "dbo.SchoolTypes");
            DropForeignKey("dbo.SchoolMetricValues", "metricID", "dbo.Metrics");
            DropForeignKey("dbo.Programs", "TypeID", "dbo.SchoolTypes");
            DropForeignKey("dbo.Metrics", "schoolLevel", "dbo.SchoolTypes");
            DropIndex("dbo.youtubeURLs", new[] { "schoolID" });
            DropIndex("dbo.SchoolProgramsValues", new[] { "programID" });
            DropIndex("dbo.SchoolProgramsValues", new[] { "schoolID" });
            DropIndex("dbo.SchoolPdatas", new[] { "schoolTypeID" });
            DropIndex("dbo.SchoolMetricValues", new[] { "metricID" });
            DropIndex("dbo.SchoolMetricValues", new[] { "schoolID" });
            DropIndex("dbo.Programs", new[] { "TypeID" });
            DropIndex("dbo.Metrics", new[] { "schoolLevel" });
            DropTable("dbo.youtubeURLs");
            DropTable("dbo.SchoolProgramsValues");
            DropTable("dbo.SchoolPdatas");
            DropTable("dbo.SchoolMetricValues");
            DropTable("dbo.Programs");
            DropTable("dbo.SchoolTypes");
            DropTable("dbo.Metrics");
            CreateIndex("dbo.SchoolsPrograms", "ProgramID");
            CreateIndex("dbo.SchoolsPrograms", "SchoolID");
            CreateIndex("dbo.Pictures", "School_id");
            AddForeignKey("dbo.Pictures", "School_id", "dbo.Schools", "ID", cascadeDelete: true);
            AddForeignKey("dbo.SchoolsPrograms", "SchoolID", "dbo.Schools", "ID", cascadeDelete: true);
            AddForeignKey("dbo.SchoolsPrograms", "ProgramID", "dbo.Programs", "ProgramID", cascadeDelete: true);
        }
    }
}
