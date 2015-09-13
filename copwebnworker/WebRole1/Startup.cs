using Microsoft.Owin;
using Owin;
using WebRole1;

[assembly: OwinStartup(typeof(Startup))]
namespace WebRole1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
