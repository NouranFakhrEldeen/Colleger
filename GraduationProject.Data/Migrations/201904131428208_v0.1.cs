namespace GraduationProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Divisions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ExtraInfo = c.String(),
                        Views = c.Int(nullable: false),
                        FacultyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Faculties", t => t.FacultyId, cascadeDelete: true)
                .Index(t => t.FacultyId);
            
            CreateTable(
                "dbo.Faculties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        logo = c.String(),
                        searchAppearancesCount = c.Int(nullable: false),
                        UniversityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Universities", t => t.UniversityId, cascadeDelete: true)
                .Index(t => t.UniversityId);
            
            CreateTable(
                "dbo.Tansiqs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Startgrade = c.Double(nullable: false),
                        Endgrade = c.Double(nullable: false),
                        Actual = c.Double(nullable: false),
                        SpecializationId = c.Int(nullable: false),
                        FacultyId = c.Int(nullable: false),
                        Division_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Faculties", t => t.FacultyId, cascadeDelete: true)
                .ForeignKey("dbo.Specializations", t => t.SpecializationId, cascadeDelete: true)
                .ForeignKey("dbo.Divisions", t => t.Division_Id)
                .Index(t => t.SpecializationId)
                .Index(t => t.FacultyId)
                .Index(t => t.Division_Id);
            
            CreateTable(
                "dbo.Specializations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Universities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Zipcode = c.String(),
                        Street = c.String(),
                        City = c.String(),
                        Governorate = c.String(),
                        Logo = c.String(),
                        Description = c.String(),
                        Views = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Interests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SearchHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Governorate = c.String(),
                        Grade = c.String(),
                        Timestamp = c.DateTime(nullable: false),
                        Anonymous = c.Boolean(nullable: false),
                        SpecializationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specializations", t => t.SpecializationId, cascadeDelete: true)
                .Index(t => t.SpecializationId);
            
            CreateTable(
                "dbo.Recommendations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SearchHistoryId = c.Int(nullable: false),
                        DivisionId = c.Int(nullable: false),
                        UserId = c.Int(),
                        Student_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Divisions", t => t.DivisionId, cascadeDelete: true)
                .ForeignKey("dbo.SearchHistories", t => t.SearchHistoryId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Student_Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.SearchHistoryId)
                .Index(t => t.DivisionId)
                .Index(t => t.UserId)
                .Index(t => t.Student_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        Password = c.String(),
                        Role = c.Int(nullable: false),
                        Grade = c.Double(),
                        Street = c.String(),
                        City = c.String(),
                        Governorate = c.String(),
                        SpecializationId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specializations", t => t.SpecializationId, cascadeDelete: true)
                .Index(t => t.SpecializationId);
            
            CreateTable(
                "dbo.InterestDivisions",
                c => new
                    {
                        Interest_Id = c.Int(nullable: false),
                        Division_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Interest_Id, t.Division_Id })
                .ForeignKey("dbo.Interests", t => t.Interest_Id, cascadeDelete: true)
                .ForeignKey("dbo.Divisions", t => t.Division_Id, cascadeDelete: true)
                .Index(t => t.Interest_Id)
                .Index(t => t.Division_Id);
            
            CreateTable(
                "dbo.SearchHistoryInterests",
                c => new
                    {
                        SearchHistory_Id = c.Int(nullable: false),
                        Interest_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SearchHistory_Id, t.Interest_Id })
                .ForeignKey("dbo.SearchHistories", t => t.SearchHistory_Id, cascadeDelete: true)
                .ForeignKey("dbo.Interests", t => t.Interest_Id, cascadeDelete: true)
                .Index(t => t.SearchHistory_Id)
                .Index(t => t.Interest_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tansiqs", "Division_Id", "dbo.Divisions");
            DropForeignKey("dbo.SearchHistories", "SpecializationId", "dbo.Specializations");
            DropForeignKey("dbo.Recommendations", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "SpecializationId", "dbo.Specializations");
            DropForeignKey("dbo.Recommendations", "Student_Id", "dbo.Users");
            DropForeignKey("dbo.Recommendations", "SearchHistoryId", "dbo.SearchHistories");
            DropForeignKey("dbo.Recommendations", "DivisionId", "dbo.Divisions");
            DropForeignKey("dbo.SearchHistoryInterests", "Interest_Id", "dbo.Interests");
            DropForeignKey("dbo.SearchHistoryInterests", "SearchHistory_Id", "dbo.SearchHistories");
            DropForeignKey("dbo.InterestDivisions", "Division_Id", "dbo.Divisions");
            DropForeignKey("dbo.InterestDivisions", "Interest_Id", "dbo.Interests");
            DropForeignKey("dbo.Divisions", "FacultyId", "dbo.Faculties");
            DropForeignKey("dbo.Faculties", "UniversityId", "dbo.Universities");
            DropForeignKey("dbo.Tansiqs", "SpecializationId", "dbo.Specializations");
            DropForeignKey("dbo.Tansiqs", "FacultyId", "dbo.Faculties");
            DropIndex("dbo.SearchHistoryInterests", new[] { "Interest_Id" });
            DropIndex("dbo.SearchHistoryInterests", new[] { "SearchHistory_Id" });
            DropIndex("dbo.InterestDivisions", new[] { "Division_Id" });
            DropIndex("dbo.InterestDivisions", new[] { "Interest_Id" });
            DropIndex("dbo.Users", new[] { "SpecializationId" });
            DropIndex("dbo.Recommendations", new[] { "Student_Id" });
            DropIndex("dbo.Recommendations", new[] { "UserId" });
            DropIndex("dbo.Recommendations", new[] { "DivisionId" });
            DropIndex("dbo.Recommendations", new[] { "SearchHistoryId" });
            DropIndex("dbo.SearchHistories", new[] { "SpecializationId" });
            DropIndex("dbo.Tansiqs", new[] { "Division_Id" });
            DropIndex("dbo.Tansiqs", new[] { "FacultyId" });
            DropIndex("dbo.Tansiqs", new[] { "SpecializationId" });
            DropIndex("dbo.Faculties", new[] { "UniversityId" });
            DropIndex("dbo.Divisions", new[] { "FacultyId" });
            DropTable("dbo.SearchHistoryInterests");
            DropTable("dbo.InterestDivisions");
            DropTable("dbo.Users");
            DropTable("dbo.Recommendations");
            DropTable("dbo.SearchHistories");
            DropTable("dbo.Interests");
            DropTable("dbo.Universities");
            DropTable("dbo.Specializations");
            DropTable("dbo.Tansiqs");
            DropTable("dbo.Faculties");
            DropTable("dbo.Divisions");
        }
    }
}
