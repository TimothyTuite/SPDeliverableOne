namespace SeniorProjectPreReq.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Schools",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Editors_ID = c.String(),
                        SchoolName = c.String(),
                        SchoolPhone = c.String(),
                        SchoolAddress = c.String(),
                        SchoolWebsite = c.String(),
                        SchoolPrincipal = c.String(),
                        StudentsEnrolled = c.Int(nullable: false),
                        ReducedLunchPercentage = c.Int(nullable: false),
                        SchoolGrade = c.String(),
                        IsCharter = c.Boolean(nullable: false),
                        MHSReadingPercentage = c.Int(nullable: false),
                        MHSMathPercentage = c.Int(nullable: false),
                        AlgebraIPassRatePercentage = c.Int(nullable: false),
                        AccCourseParticipationPercentage = c.Int(nullable: false),
                        PSRReadyReadingPercentage = c.Int(nullable: false),
                        PSReadyMathPercentage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Schools");
        }
    }
}
