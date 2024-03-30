using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DACS_WebNuocHoa.Startup))]
namespace DACS_WebNuocHoa
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
