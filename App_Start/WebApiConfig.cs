using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net.Http;
using System.Web.Http.Cors;
using System.Net.Http.Headers;

namespace WebApplication1
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //web api json response configuration
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            //wbe api cors enabled
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            // Web API routes
            config.MapHttpAttributeRoutes();


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
