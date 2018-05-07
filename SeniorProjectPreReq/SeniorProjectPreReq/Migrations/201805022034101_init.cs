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
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SchoolMetricValues",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        schoolID = c.Int(nullable: false),
                        metricID = c.Int(nullable: false),
                        value = c.Single(nullable: false),
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
                        SchoolName = c.String(nullable: false),
                        SchoolPhone = c.String(nullable: false),
                        SchoolAddress = c.String(nullable: false),
                        SchoolWebsite = c.String(nullable: false),
                        SchoolPrincipal = c.String(nullable: false),
                        schoolTypeID = c.Int(nullable: false),
                        Approved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SchoolTypes", t => t.schoolTypeID, cascadeDelete: true)
                .Index(t => t.schoolTypeID);
            
            CreateTable(
                "dbo.SchoolTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
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
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        schoolID = c.Int(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SchoolPdatas", t => t.schoolID)
                .Index(t => t.schoolID)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
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
            DropForeignKey("dbo.youtubeURLs", "schoolID", "dbo.SchoolPdatas");
            DropForeignKey("dbo.AspNetUsers", "schoolID", "dbo.SchoolPdatas");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SchoolProgramsValues", "programID", "dbo.Programs");
            DropForeignKey("dbo.SchoolProgramsValues", "schoolID", "dbo.SchoolPdatas");
            DropForeignKey("dbo.SchoolMetricValues", "schoolID", "dbo.SchoolPdatas");
            DropForeignKey("dbo.SchoolPdatas", "schoolTypeID", "dbo.SchoolTypes");
            DropForeignKey("dbo.SchoolMetricValues", "metricID", "dbo.Metrics");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.youtubeURLs", new[] { "schoolID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "schoolID" });
            DropIndex("dbo.SchoolProgramsValues", new[] { "programID" });
            DropIndex("dbo.SchoolProgramsValues", new[] { "schoolID" });
            DropIndex("dbo.SchoolPdatas", new[] { "schoolTypeID" });
            DropIndex("dbo.SchoolMetricValues", new[] { "metricID" });
            DropIndex("dbo.SchoolMetricValues", new[] { "schoolID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.youtubeURLs");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.SchoolProgramsValues");
            DropTable("dbo.SchoolTypes");
            DropTable("dbo.SchoolPdatas");
            DropTable("dbo.SchoolMetricValues");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Programs");
            DropTable("dbo.Metrics");
        }
    }
}
