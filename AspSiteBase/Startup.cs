using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AspSiteBase.Startup))]
namespace AspSiteBase
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
