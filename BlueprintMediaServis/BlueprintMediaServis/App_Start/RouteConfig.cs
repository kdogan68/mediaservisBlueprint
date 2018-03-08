using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BlueprintMediaServis
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "Custom",
               url: "UpdateMagazine",
               defaults: new { controller = "UpdateMagazine", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "Custom2",
                url: "MenuPage",
                defaults: new { controller = "MenuPage", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Custom4",
                url: "Magazine",
                defaults: new { controller = "Magazine", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Custom5",
                url: "Catalog",
                defaults: new { controller = "Catalog", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Custom6",
                url: "Video",
                defaults: new { controller = "Video", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "Custom7",
               url: "Menu",
               defaults: new { controller = "Menu", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "Custom8",
               url: "PagesList",
               defaults: new { controller = "PagesList", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "Default",
               url: "Home",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
                name: "Custom3",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );

            

           
        }
    }
}
