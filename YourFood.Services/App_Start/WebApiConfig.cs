namespace YourFood.Services
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using Microsoft.Owin.Security.OAuth;
    using Newtonsoft.Json;
    using YourFood.Models;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<AvailabilityProduct>("AvailabilityProducts");
            builder.EntitySet<CatalogProduct>("CatalogProducts");
            builder.EntitySet<Product>("Products");
            builder.EntitySet<Recipe>("Recipes");
            builder.EntitySet<RecipeCategory>("RecipeCategories"); 
            builder.EntitySet<ProductCategory>("ProductCategories"); 
            config.Routes.MapODataServiceRoute("api", "api", builder.GetEdmModel());

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            config.AddODataQueryFilter();
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}