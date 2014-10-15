namespace YourFood.Services
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.Http;
    using Microsoft.Owin;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.OwinHost;
    using Ninject.Web.WebApi;
    using Ninject.Web.WebApi.OwinHost;
    using Owin;
    using YourFood.Data.DbContext;
    using YourFood.Data.UoW;

    [assembly: OwinStartup(typeof(Startup))]
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
            //app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(GlobalConfiguration.Configuration);

            CreateKernel();
        }

        private static StandardKernel CreateKernel()
        {
            //var kernel = new StandardKernel();
            //RegisterMappings(kernel);
            //return kernel;
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

           RegisterMappings(kernel);

            // Install our Ninject-based IDependencyResolver into the Web API config
            GlobalConfiguration.Configuration.DependencyResolver = new App_Start.NinjectDependencyResolver(kernel);

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