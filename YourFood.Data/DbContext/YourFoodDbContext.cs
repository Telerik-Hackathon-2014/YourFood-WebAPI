namespace YourFood.Data.DbContext
{
    using System;
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using YourFood.Common;
    using YourFood.Data.Migrations;
    using YourFood.Models;

    public class YourFoodDbContext : IdentityDbContext<User>, IYourFoodDbContext
    {
        public YourFoodDbContext()
            : base(ConnectionStrings.CloudDatabaseConnection, throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<YourFoodDbContext, Configuration>());
        }
        
        public IDbSet<AvailabilityProduct> AvailabilityProducts { get; set; }

        public IDbSet<CatalogProduct> CatalogProducts { get; set; }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<ProductCategory> ProductCategories { get; set; }

        public IDbSet<Recipe> Recipes { get; set; }

        public IDbSet<RecipeCategory> RecipeCategoriess { get; set; }

        public IDbSet<RecipeProduct> RecipeProducts { get; set; }

        public IDbSet<RecipeUsageRecord> RecipeUsageRecords { get; set; }

        public IDbSet<ShoppingList> ShoppingLists { get; set; }

        public static YourFoodDbContext Create()
        {
            return new YourFoodDbContext();
        }

        public IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}
