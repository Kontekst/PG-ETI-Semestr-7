using System.Web.Mvc;
using System.Web.Routing;

namespace MiniFacebook
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "LoggedUserMainPage",
                url: "Index",
                defaults: new { controller = "Authorization", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Authorization", action = "LoginPage" }
            );


        }
    }
}
