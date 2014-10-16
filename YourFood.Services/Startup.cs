namespace YourFood.Services
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Web.Http;
    using Microsoft.Owin;
    using Ninject;
    using Ninject.Web.Common.OwinHost;
    using Ninject.Web.WebApi;
    using Owin;
    using YourFood.Data.DbContext;
    using YourFood.Data.UoW;

    [assembly: OwinStartup(typeof(Startup))]

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
            app.UseNinjectMiddleware(CreateKernel);
        }

        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            RegisterMappings(kernel);
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
            return kernel;
        }

        private static void RegisterMappings(StandardKernel kernel)
        {
            kernel.Bind<IYourFoodData>()
                  .To<YourFoodData>()
                  .WithConstructorArgument("context", c => new YourFoodDbContext());
        }
    }
}