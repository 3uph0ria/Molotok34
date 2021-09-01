using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Molotok34.Startup))]
namespace Molotok34
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
