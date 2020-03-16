using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Routing;
using Microsoft.Web.Http.Routing;
using ugame.api.CustomHandler;

namespace ugame.api
{
    public static class WebApiConfig
    {
        public static void RegisterWebApi(this HttpConfiguration config)
        {
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            var constraintResolver = new DefaultInlineConstraintResolver() { ConstraintMap = { ["apiVersion"] = typeof(ApiVersionRouteConstraint) } };

            // Web API configuration and services
            config.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new Microsoft.Web.Http.ApiVersion(1, 0);
            });

            // Web API routes
            config.MapHttpAttributeRoutes(constraintResolver);

            ////Registering GlobalExceptionHandler
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.RegisterUnity();
        }
    }
}
