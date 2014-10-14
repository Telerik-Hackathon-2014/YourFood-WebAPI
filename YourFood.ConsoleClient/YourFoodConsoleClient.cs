namespace YourFood.ConsoleClient
{
    using System;
    using System.Linq;
    using YourFood.Data.UoW;

    public class YourFoodConsoleClient
    {
        private static readonly IYourFoodData yourFoodData = new YourFoodData();

        internal static void Main()
        {
            Console.WriteLine(yourFoodData.Users.All().Count());
        }
    }
}