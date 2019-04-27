namespace GraduationProject.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GraduationProject.Data.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GraduationProject.Data.DatabaseContext context)
        {
            context.Interests.AddOrUpdate(new Entities.Interest() { Id = 1, name = "software engineering"});
            context.Interests.AddOrUpdate(new Entities.Interest() { Id = 2, name = "business" });

            context.Specializations.AddOrUpdate(new Entities.Specialization() {  Id = 1 , Name = "3lm 3lom "});
            context.Specializations.AddOrUpdate(new Entities.Specialization() { Id = 2, Name = "3lm rdyada " });
            context.Specializations.AddOrUpdate(new Entities.Specialization() { Id = 3, Name = "adaby " });
            context.Users.AddOrUpdate(new Entities.User() {Id = 1 , Email= "admin@gmail.com" , Password="123456678" , Firstname= "admin" , Lastname="" , Role= Entities.Role.admin , Recommendations = null});

            context.Universities.AddOrUpdate(new Entities.University() { Id = 1 ,Name = "Uni1" , City= "nasr city" , Governorate = "Cairo" , Description= "" , Street = "test" , Zipcode= null , Logo = null });
            context.Faculties.AddOrUpdate(new Entities.Faculty() { Id = 1, Name = "faculty1", UniversityId = 1, Description = null, logo = null, searchAppearancesCount = 1 });
            context.Division.AddOrUpdate(new Entities.Division() { Id = 1, Name = "Divison1", FacultyId = 1, Description = null });
        }
    }
}
