using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;

namespace ugame.api.Crosscutting
{
    public class UnityResolver : IDependencyResolver
    {
        protected IUnityContainer container;

        public UnityResolver(IUnityContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException re)
            {
                return null;
            }
        }

        public T GetService<T>()
        {
            try
            {
                return (T)container.Resolve(typeof(T));
            }
            catch (ResolutionFailedException re)
            {
                return default(T);
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException re)
            {
                return new List<object>();
            }
        }

        public IEnumerable<T> GetServices<T>()
        {
            try
            {
                return container.ResolveAll(typeof(T)).Cast<T>();
            }
            catch (ResolutionFailedException re)
            {
                return new List<T>();
            }
        }

        public IDependencyScope BeginScope()
        {
            var child = container.CreateChildContainer();
            return new UnityResolver(child);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            container.Dispose();
        }
    }
}
