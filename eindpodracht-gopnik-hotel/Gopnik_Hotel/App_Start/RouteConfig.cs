using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Gopnik_Hotel
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                 name: "Kamer",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "Kamers", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Boekings",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Boekings", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Stap2",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "KamerBoeken", action = "Stap2", id = UrlParameter.Optional }
            );
        }
    }
}
