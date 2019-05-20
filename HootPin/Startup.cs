using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HootPin.Startup))]
namespace HootPin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
