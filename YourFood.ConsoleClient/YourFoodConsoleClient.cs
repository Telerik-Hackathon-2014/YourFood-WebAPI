namespace YourFood.ConsoleClient
{
    using System;
    using System.Linq;
    using YourFood.Data.DbContext;
    using YourFood.Data.UoW;
    using YourFood.Models;
    using YourFood.Models.Enums;

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
                LifetimeInDays = 5,
                Product = new Product()
                {
                    Category = new ProductCategory()
                    {
                        Name = "Food"
                    },
                    ImageUrl = "sample",
                    UnitType = UnitType.Kilograms,
                    MeasurementUnit = 5,
                    Name = "eggs"
                }
            });

            yourFoodData.SaveChanges();

            Console.WriteLine(yourFoodData.CatalogProducts.All().First().LifetimeInDays);
        }
    }
}