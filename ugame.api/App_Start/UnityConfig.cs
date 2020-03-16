using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Web.Http;
using Michelin.ePC.Library.Unity;
using Michelin.ePC.Library.Unity.Configurations;
using Michelin.ePC.Library.Unity.Enums;
using Microsoft.Practices.Unity;
using ugame.api.Crosscutting;

namespace ugame.api
{
    public static class UnityConfig
    {
        public static void RegisterUnity(this HttpConfiguration config)
        {
            var iocContainerMember = typeof(UnityFactory).GetField("_unityContainer", BindingFlags.Static | BindingFlags.NonPublic);
            var dicoMember = typeof(IoCUnityContainer).GetField("_containersDictionary", BindingFlags.NonPublic | BindingFlags.Instance);
            var iocContainer = iocContainerMember.GetValue(null);
            var dico = dicoMember.GetValue(iocContainer) as IDictionary<string, IUnityContainer>;

            IUnityContainer container = null;
            if (UnityConfigurationManager.UseMockContext)
                container = dico[ContainerType.MockContainer.ToString()];
            else
                container = dico[ContainerType.RealContainer.ToString()];

            // NOT MANDATORY : RegisterController(container);

            config.DependencyResolver = new UnityResolver(container);
        }

        private static void RegisterController(IUnityContainer container)
        {
            foreach (var type in typeof(UnityConfig).Assembly.GetTypes())
            {
                if (type.Name.EndsWith("Controller", true, CultureInfo.InvariantCulture))
                {
                    container.RegisterType(type, type, new TransientLifetimeManager());
                }
            }
        }
    }
}
