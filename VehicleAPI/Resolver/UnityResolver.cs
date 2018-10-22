using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Unity;
using Unity.Exceptions;

/// <summary>
/// Web API defines the IDependencyResolver interface for resolving dependencies. When web API creates a controller instance,
/// it first calls an IDependencyResolver method called GetService, and passes in the controller type. We will implement GetService
/// here as well as other methods in, and inherited by IDependencyResolver to create the controller and resolve any dependencies.
/// </summary>
namespace VehicleAPI.Resolver
{
    /// <summary>
    /// This implementation of the IDependencyResolver interface will wrap a Unity container to manage dependencies.
    /// The types will be registered with the container, which will be used to create the objects.
    /// The container automatically figures out the dependency relations.   
    /// </summary>
    public class UnityResolver : IDependencyResolver
    {
        protected IUnityContainer container;

        public UnityResolver(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            this.container = container;
        }

        /// <summary>
        /// Creates one instance of a type.
        /// </summary>
        /// <param name="serviceType">type to create</param>
        /// <returns>instance or null</returns>
        public object GetService(Type serviceType)
        {
            // return null if can't resolve a type
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        /// <summary>
        /// creates a collection of objects of a specified type.
        /// </summary>
        /// <param name="serviceType">type to create</param>
        /// <returns>collection</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            // return empty list if can't resolve type
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        /// <summary>
        /// Controllers are created per request. IDependencyResolver uses the concept of a scope To manage object lifetimes.
        /// The dependency resolver has global scope. When Web API creates a controller it calls this method which returns a child scope.
        /// Web API then calls GetService on the child scope to create the controller. When request is complete, Web API calls Dispose
        /// on the child scope.
        /// </summary>
        /// <returns>IDependencyScope</returns>
        public IDependencyScope BeginScope()
        {
            var child = container.CreateChildContainer();
            return new UnityResolver(child);
        }
        /// <summary>
        /// Used to dispose of the controller's dependencies.
        /// </summary>
        public void Dispose()
        {
            container.Dispose();
        }
    }
}