using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UniversityManagementMVC.Startup))]
namespace UniversityManagementMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
