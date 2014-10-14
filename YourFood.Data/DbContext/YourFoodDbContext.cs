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
            : base(ConnectionStrings.DefaultConnection, throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<YourFoodDbContext, Configuration>());
        }
        
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