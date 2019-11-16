using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(vegetable.Startup))]
namespace vegetable
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
