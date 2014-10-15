namespace YourFood.Services
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.WebApi;
    using YourFood.Data.DbContext;
    using YourFood.Data.UoW;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //CreateKernel();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            this.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            // register all your dependencies on the kernel container
            RegisterMappings(kernel);

            // register the dependency resolver passing the kernel container
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