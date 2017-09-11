using System.Web.Http;

namespace backend.WebApi.config
{
    public class RouteConfig
    {
        public static void Register(HttpRouteCollection route)
        {
            route.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new {id = RouteParameter.Optional});

            //route.MapHttpRoute(
            //    name: "ActionApi",
            //    routeTemplate: "api/{controller}/{action}/{id}",
            //    defaults: new {id = RouteParameter.Optional}
            //    );
        }
    }
}
