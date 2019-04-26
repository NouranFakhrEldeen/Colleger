namespace GraduationProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v02 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tansiqs", "FacultyId", "dbo.Faculties");
            DropIndex("dbo.Tansiqs", new[] { "FacultyId" });
            RenameColumn(table: "dbo.Tansiqs", name: "Division_Id", newName: "DivisionId");
            RenameIndex(table: "dbo.Tansiqs", name: "IX_Division_Id", newName: "IX_DivisionId");
            AlterColumn("dbo.Tansiqs", "FacultyId", c => c.Int());
            CreateIndex("dbo.Tansiqs", "FacultyId");
            AddForeignKey("dbo.Tansiqs", "FacultyId", "dbo.Faculties", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tansiqs", "FacultyId", "dbo.Faculties");
            DropIndex("dbo.Tansiqs", new[] { "FacultyId" });
            AlterColumn("dbo.Tansiqs", "FacultyId", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.Tansiqs", name: "IX_DivisionId", newName: "IX_Division_Id");
            RenameColumn(table: "dbo.Tansiqs", name: "DivisionId", newName: "Division_Id");
            CreateIndex("dbo.Tansiqs", "FacultyId");
            AddForeignKey("dbo.Tansiqs", "FacultyId", "dbo.Faculties", "Id", cascadeDelete: true);
        }
    }
}
