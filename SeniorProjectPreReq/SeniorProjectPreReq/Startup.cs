using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SeniorProjectPreReq.Startup))]
namespace SeniorProjectPreReq
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
