using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Mvc.Oefenfirma.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // "/" Lists the first page of products from all categories
            // If I decomment this, the startup-page is the productlist instead of the homepage.
            routes.MapRoute(
                null,
                "Product",
                new { controller = "Product", action = "Index", categorie = (string)null, page = 1 }
            );

            // "/Page2" Lists the specified page (in this case, page 2), showing items from all categories
            routes.MapRoute(
                null,
                "Product/Page{page}",
                new { controller = "Product", action = "Index", categorie = (string)null },
                new { page = @"\d+" },
                namespaces: new[] { "Mvc.Oefenfirma.Web.Controllers" }
            );

            //// "/Account" special route, because of the "/Soccer" interference...
            // De extra routings heb ik "Product/..." genoemd ipv {controller}/...
            //routes.MapRoute(
            //    name: null,
            //    url: "Account/{action}/{id}",
            //    defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
            //    namespaces: new[] { "Mvc.Oefenfirma.Web.Controllers" }
            //);


            //// "/Soccer" Shows the first page of items from a specific category(in this case, the Soccer category)
            routes.MapRoute(
                name: "ProductRoute",
                url: "Product/{category}",
                defaults: new { controller = "Product", action = "Index", category = UrlParameter.Optional, page = 1 },
                namespaces: new[] { "Mvc.Oefenfirma.Web.Controllers" }
            );

            //// "/Soccer/Page2" Shows the specified page (in this case, page 2) of items from the specified category 
            ////                 (in this case, Soccer)
            routes.MapRoute(
                null,
                "Product/{category}/Page{page}",
                new { controller = "Product", action = "Index", category = UrlParameter.Optional },
                new { page = @"\d+" },
                new[] { "Mvc.Oefenfirma.Web.Controllers" }
            );

            //routes.MapRoute(null, "{controller}/{action}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Mvc.Oefenfirma.Web.Controllers" }
            );

        }
    }
}
