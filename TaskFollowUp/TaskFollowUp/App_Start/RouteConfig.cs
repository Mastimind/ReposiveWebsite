using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Web.Routing;

namespace TaskFollowUp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

           
            routes.MapRoute(
               name: "ScriptBundle",
               url: "scripts/application.js",
               defaults: new { controller = "Resource", action = "JavaScriptBundle" }
           );

            routes.MapRoute(
               name: "CssBundle",
               url: "content/css-bundle.css",
               defaults: new { controller = "Resource", action = "CssBundle" }
           );

            routes.MapRoute(
                name: "DesktopManifest",
                url: "Desktop.manifest",
                defaults: new { controller = "Resource", action = "DesktopManifest" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}