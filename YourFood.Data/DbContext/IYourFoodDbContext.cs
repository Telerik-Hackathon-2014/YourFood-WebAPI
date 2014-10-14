namespace YourFood.Data.DbContext
{
    using System.Data.Entity;
    using YourFood.Models;

    public interface IYourFoodDbContext : IDbContext
    {
        IDbSet<User> Users { get; set; }
    }
}