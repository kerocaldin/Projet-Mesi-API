using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Description;
using Swashbuckle.Application;

namespace ugame.api
{
    public static class SwaggerConfig
    {
        static string XmlCommentsFilePath
        {
            get
            {
                var basePath = System.AppDomain.CurrentDomain.BaseDirectory;
                var fileName = typeof(SwaggerConfig).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine(basePath, fileName);
            }
        }

        public static void RegisterSwagger(this HttpConfiguration config)
        {
            var apiExplorer = config.AddVersionedApiExplorer(o =>
            {
                o.GroupNameFormat = "'v'VV";
                o.SubstituteApiVersionInUrl = true;
                o.SubstitutionFormat = "VV";
            });


            config.EnableSwagger("{apiVersion}/swagger",
                    c =>
                    {

                        c.RootUrl((request) =>
                        {
                            return request.RequestUri.GetLeftPart(UriPartial.Authority)
                                   + request.GetRequestContext().VirtualPathRoot.TrimEnd('/');
                        });

                        c.MultipleApiVersions(
                            (apiDescription, version) => apiDescription.GetGroupName() == version,
                            info =>
                            {
                                foreach (var group in apiExplorer.ApiDescriptions)
                                {
                                    info.Version(group.Name, $"ugame.api Web API {group.ApiVersion}");
                                }
                            });
                        c.IgnoreObsoleteProperties();
                    })
                .EnableSwaggerUi(c =>
                {
                    c.EnableDiscoveryUrlSelector();
                });
        }
    }
}
