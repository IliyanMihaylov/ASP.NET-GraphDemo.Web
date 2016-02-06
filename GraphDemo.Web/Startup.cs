using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GraphDemo.Web.Startup))]
namespace GraphDemo.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
