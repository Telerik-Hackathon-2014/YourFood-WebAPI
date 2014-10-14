namespace YourFood.Services
{
    using System;
    using System.Linq;
    using Microsoft.Owin;
    using Owin;

    [assembly: OwinStartup(typeof(Startup))]
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}