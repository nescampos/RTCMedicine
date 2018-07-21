using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Telemedicina.Web.Startup))]
namespace Telemedicina.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
