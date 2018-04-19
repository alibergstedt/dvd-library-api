using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DvdLibrary.UI.Startup))]
namespace DvdLibrary.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
