using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SP_ASPNET_1.Startup))]
namespace SP_ASPNET_1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
