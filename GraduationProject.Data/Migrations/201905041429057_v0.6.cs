namespace GraduationProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v06 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CoursesDivisions");
            AddPrimaryKey("dbo.CoursesDivisions", new[] { "Division_Id", "Courses_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.CoursesDivisions");
            AddPrimaryKey("dbo.CoursesDivisions", new[] { "Courses_Id", "Division_Id" });
        }
    }
}
