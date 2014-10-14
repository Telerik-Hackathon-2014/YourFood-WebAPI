namespace YourFood.ConsoleClient
{
    using System;
    using System.Linq;
    using YourFood.Data.DbContext;
    using YourFood.Data.UoW;
    using YourFood.Models;

    public class YourFoodConsoleClient
    {
        private static readonly IYourFoodData yourFoodData = new YourFoodData(new YourFoodDbContext());

        internal static void Main()
        {
            Console.WriteLine(yourFoodData.Users.All().Count());

            SeedProductInDatabase();
        }
 
        private static void SeedProductInDatabase()
        {
            if (yourFoodData.CatalogProducts.All().Any())
            {
                return;
            }

            yourFoodData.CatalogProducts.Add(new CatalogProduct()
            {
                LifeTimePeriod = new TimeSpan(5, 20, 5, 2),
                Product = new Product()
                {
                    Category = new ProductCategory()
                    {
                        Name = "Food"
                    },
                    ImageUrl = "sample",
                    MeasurementUnit = "kg",
                    Name = "eggs"
                }
            });

            yourFoodData.SaveChanges();

            Console.WriteLine(yourFoodData.CatalogProducts.All().First().LifeTimePeriod);
        }
    }
}