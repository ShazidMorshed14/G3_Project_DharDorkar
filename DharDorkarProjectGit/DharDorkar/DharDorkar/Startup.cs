using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DharDorkar.Startup))]
namespace DharDorkar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
