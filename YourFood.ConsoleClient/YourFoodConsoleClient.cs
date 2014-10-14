namespace YourFood.ConsoleClient
{
    using System;
    using System.Linq;
    using YourFood.Data.DbContext;
    using YourFood.Data.UoW;

    public class YourFoodConsoleClient
    {
        private static readonly IYourFoodData yourFoodData = new YourFoodData(new YourFoodDbContext());

        internal static void Main()
        {
            Console.WriteLine(yourFoodData.Users.All().Count());
        }
    }
}