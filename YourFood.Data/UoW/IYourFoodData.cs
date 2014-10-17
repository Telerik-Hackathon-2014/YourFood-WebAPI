namespace YourFood.Data.UoW
{
    using System;
    using System.Linq;
    using YourFood.Data.Repositories;
    using YourFood.Models;

    public interface IYourFoodData : IDisposable
    {
        IGenericRepository<User> Users { get; }

        IGenericRepository<AvailabilityProduct> AvailabilityProducts { get; }

        IGenericRepository<CatalogProduct> CatalogProducts { get; }

        IGenericRepository<Product> Products { get; }

        IGenericRepository<ProductCategory> ProductCategories { get; }

        IGenericRepository<Recipe> Recipes { get; }

        IGenericRepository<RecipeCategory> RecipeCategories { get; }

        IGenericRepository<RecipeProduct> RecipeProducts { get; }

        IGenericRepository<RecipeUsageRecord> RecipeUsageRecords { get; }

        IGenericRepository<ShoppingList> ShoppingLists { get; }

        IGenericRepository<ProductFrequency> ProductFrequencies { get; }

        int SaveChanges();
    }
}