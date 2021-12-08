using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TotallyNotGuFundMe.Startup))]
namespace TotallyNotGuFundMe
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
