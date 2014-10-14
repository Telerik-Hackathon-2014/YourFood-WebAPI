namespace YourFood.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using YourFood.Data.DbContext;

    internal sealed class Configuration : DbMigrationsConfiguration<YourFoodDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }
    }
}