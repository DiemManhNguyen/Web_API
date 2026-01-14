using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace web_api_1771020152
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // 1. Bật CORS (Cho phép mọi nguồn truy cập)
            config.EnableCors(new System.Web.Http.Cors.EnableCorsAttribute("*", "*", "*"));

            // Web API configuration and services
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}