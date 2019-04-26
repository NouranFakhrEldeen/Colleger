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
            context.Interests.Add(new Entities.Interest() { Id = 1, name = "software engineering"});
            context.Interests.Add(new Entities.Interest() { Id = 2, name = "business" });


            context.Specializations.Add(new Entities.Specialization() {  Id = 1 , Name = "3lm 3lom "});
            context.Specializations.Add(new Entities.Specialization() { Id = 2, Name = "3lm rdyada " });
            context.Specializations.Add(new Entities.Specialization() { Id = 3, Name = "adaby " });
            context.Users.Add(new Entities.User() {Id = 1 , Email= "admin@gmail.com" , Password="123456678" , Firstname= "admin" , Lastname="" , Role= Entities.Role.admin , Recommendations = null});



        }
    }
}
