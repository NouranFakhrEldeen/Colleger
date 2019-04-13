using System;
using Microsoft.Owin;
using Owin;
[assembly: OwinStartupAttribute(typeof(GraduationProject.MVC.Startup))]
namespace GraduationProject.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        
    }
}