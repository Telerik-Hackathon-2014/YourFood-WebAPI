namespace YourFood.Data.UoW
{
    using System;
    using System.Linq;
    using YourFood.Data.Repositories;
    using YourFood.Models;

    public interface IYourFoodData : IDisposable
    {
        IGenericRepository<User> Users { get; }

        int SaveChanges();
    }
}