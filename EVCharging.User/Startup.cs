using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EVCharging.User.Startup))]
namespace EVCharging.User
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
