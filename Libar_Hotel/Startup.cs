using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Libar_Hotel.Startup))]
namespace Libar_Hotel
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
