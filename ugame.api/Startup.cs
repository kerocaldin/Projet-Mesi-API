using System.Web.Http;
using Microsoft.Owin;
using Owin;

namespace ugame.api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var configuration = new HttpConfiguration();

            configuration.RegisterUnity();
            configuration.RegisterWebApi();
            configuration.RegisterSwagger();

            app.UseWebApi(configuration);
        }
    }
}
