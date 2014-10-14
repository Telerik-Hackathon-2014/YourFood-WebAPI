namespace YourFood.Data.UoW
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using YourFood.Data.Repositories;
    using YourFood.Models;

    public class YourFoodData : IYourFoodData
    {
        private readonly DbContext context;
        private readonly IDictionary<Type, object> repositories = new Dictionary<Type, object>();

        public YourFoodData(DbContext context)
        {
            this.context = context;
        }

        public IGenericRepository<User> Users
        {
            get
            {
                return this.GetRepository<User>();
            }
        }

        public IGenericRepository<AvailabilityProduct> AvailabilityProducts
        {
            get
            {
                return this.GetRepository<AvailabilityProduct>();
            }
        }

        public IGenericRepository<CatalogProduct> CatalogProducts
        {
            get
            {
                return this.GetRepository<CatalogProduct>();
            }
        }

        public IGenericRepository<Product> Products
        {
            get
            {
                return this.GetRepository<Product>();
            }
        }

        public IGenericRepository<ProductCategory> ProductCategories
        {
            get
            {
                return this.GetRepository<ProductCategory>();
            }
        }

        public IGenericRepository<Recipe> Recipes
        {
            get
            {
                return this.GetRepository<Recipe>();
            }
        }

        public IGenericRepository<RecipeCategory> RecipeCategoriess
        {
            get
            {
                return this.GetRepository<RecipeCategory>();
            }
        }

        public IGenericRepository<RecipeProduct> RecipeProducts
        {
            get
            {
                return this.GetRepository<RecipeProduct>();
            }
        }

        public IGenericRepository<RecipeUsageRecord> RecipeUsageRecords
        {
            get
            {
                return this.GetRepository<RecipeUsageRecord>();
            }
        }

        public IGenericRepository<ShoppingList> ShoppingLists
        {
            get
            {
                return this.GetRepository<ShoppingList>();
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.context != null)
                {
                    this.context.Dispose();
                }
            }
        }

        private IGenericRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);

            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var typeOfRepository = typeof(GenericRepository<T>);

                this.repositories.Add(typeOfModel, Activator.CreateInstance(typeOfRepository, this.context));
            }

            return (IGenericRepository<T>)this.repositories[typeOfModel];
        }
    }
}