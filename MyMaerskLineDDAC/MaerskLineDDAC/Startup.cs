using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MaerskLineDDAC.Startup))]
namespace MaerskLineDDAC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
