using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PropertyManagament.Startup))]
namespace PropertyManagament
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
