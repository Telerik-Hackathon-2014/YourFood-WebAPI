namespace YourFood.Data.DbContext
{
    using System.Data.Entity;
    using YourFood.Models;

    public interface IYourFoodDbContext : IDbContext
    {
        IDbSet<User> Users { get; set; }

        IDbSet<AvailabilityProduct> AvailabilityProducts { get; set; }

        IDbSet<CatalogProduct> CatalogProducts { get; set; }

        IDbSet<Product> Products { get; set; }

        IDbSet<ProductCategory> ProductCategories { get; set; }

        IDbSet<Recipe> Recipes { get; set; }

        IDbSet<RecipeCategory> RecipeCategoriess { get; set; }

        IDbSet<RecipeProduct> RecipeProducts { get; set; }

        IDbSet<RecipeUsageRecord> RecipeUsageRecords { get; set; }

        IDbSet<ShoppingList> ShoppingLists { get; set; }
    }
}