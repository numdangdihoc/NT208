using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bookstore
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            /*routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Login",
                url: "signin.html",
                defaults: new { controller = "Home", action = "SignIn", id = UrlParameter.Optional },
                namespaces:new string[] { "Bookstore.Controllers" }
            );
            routes.MapRoute(
                name: "SignUp",
                url: "signup.html",
                defaults: new { controller = "Home", action = "SignUp", id = UrlParameter.Optional },
                namespaces: new string[] { "Bookstore.Controllers" }
            );*/

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
