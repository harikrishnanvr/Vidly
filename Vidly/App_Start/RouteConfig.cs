using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Vidly
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapMvcAttributeRoutes();
            ////old method of routing will be changing this to attribute routing
            //routes.MapRoute(
            //    "MoviesByReleaseDate",
            //    "Movies/released/{year}/{month}",
            //    new { controller = "Movies", action= "ByReleaseDate" },
            //    new {year=@"\d{4}",month=@"\d{2}"}// added constraints that year should contain 4 digits and date should contain 2 digit
            //    );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
