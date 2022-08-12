using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bilet_Online.Startup))]
namespace Bilet_Online
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
