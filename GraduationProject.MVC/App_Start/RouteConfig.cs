﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GraduationProject.MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();
            routes.MapRoute("api/interests", "api/interests", new
            {
                controller = "Interests",
                action = "API"
            });
            routes.MapRoute("api/faculties/{id}", "api/faculties/{id}", new
            {
                controller = "Faculties",
                action = "API"
            });
            routes.MapRoute("api/specializations", "api/specializations", new
            {
                controller = "Specializations",
                action = "API"
            });
            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );
        }
    }
}
