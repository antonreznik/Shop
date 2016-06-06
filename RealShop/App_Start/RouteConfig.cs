using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RealShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "null",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

           // routes.MapRoute(
           //    name: null,
           //    url: "{controller}/{action}/{value1}/{value2}/{value3}"
           //);

           // routes.MapRoute(
           //    name: null,
           //    url: "{controller}/{action}/{value1}/{value2}"
           //);

           // routes.MapRoute(
           //     name: null,
           //     url: "{controller}/{action}/{value}"
           // );

           // routes.MapRoute(
           //     name: null,
           //     url: "{controller}/{action}",   
           //     defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           // );

            
        }
    }
}