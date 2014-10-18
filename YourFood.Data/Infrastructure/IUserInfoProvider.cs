namespace YourFood.Data.Infrastructure
{
    using System;
    using System.Linq;

    public interface IUserInfoProvider
    {
        bool IsUserAuthenticated();

        string GetUsername();

        string GetUserId();
    }
}