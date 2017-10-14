using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(graph_views_exploration.Startup))]
namespace graph_views_exploration
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
