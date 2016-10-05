using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EF_Relations.Startup))]
namespace EF_Relations
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
