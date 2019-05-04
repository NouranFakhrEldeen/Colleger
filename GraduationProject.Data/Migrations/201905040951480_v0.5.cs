namespace GraduationProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v05 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CoursesDivisions",
                c => new
                    {
                        Courses_Id = c.Int(nullable: false),
                        Division_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Courses_Id, t.Division_Id })
                .ForeignKey("dbo.Courses", t => t.Courses_Id, cascadeDelete: true)
                .ForeignKey("dbo.Divisions", t => t.Division_Id, cascadeDelete: true)
                .Index(t => t.Courses_Id)
                .Index(t => t.Division_Id);
            
            AddColumn("dbo.Divisions", "Fees", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CoursesDivisions", "Division_Id", "dbo.Divisions");
            DropForeignKey("dbo.CoursesDivisions", "Courses_Id", "dbo.Courses");
            DropIndex("dbo.CoursesDivisions", new[] { "Division_Id" });
            DropIndex("dbo.CoursesDivisions", new[] { "Courses_Id" });
            DropColumn("dbo.Divisions", "Fees");
            DropTable("dbo.CoursesDivisions");
            DropTable("dbo.Courses");
        }
    }
}
