namespace GraduationProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v03 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SearchHistories", "Grade", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SearchHistories", "Grade", c => c.String());
        }
    }
}
