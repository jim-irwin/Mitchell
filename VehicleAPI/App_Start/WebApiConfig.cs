using System.Web.Http;
using Unity;
using VehicleAPI.Models;

namespace VehicleAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // The dependency resolver is attached to the HttpConfiguration object
            // Create a new Unity container 
            var container = new UnityContainer();
            // Register the IVehicleRepository interface with Unity
            container.RegisterType<IVehicleRepository, VehicleRepository>(new Unity.Lifetime.HierarchicalLifetimeManager());
            // create a UnityResolver
            config.DependencyResolver = new Resolver.UnityResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Output JSON
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { Controller = "Vehicles",  id = RouteParameter.Optional }
            );
        }
    }
}
