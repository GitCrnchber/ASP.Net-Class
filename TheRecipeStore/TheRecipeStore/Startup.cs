using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TheRecipeStore.Startup))]
namespace TheRecipeStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
