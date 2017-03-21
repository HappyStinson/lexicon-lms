using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lexicon_LMS.Startup))]
namespace Lexicon_LMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
