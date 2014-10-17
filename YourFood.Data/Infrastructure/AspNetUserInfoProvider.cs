namespace YourFood.Data.Infrastructure
{
    using System;
    using System.Linq;
    using System.Threading;
    using Microsoft.AspNet.Identity;

    public class AspNetUserInfoProvider : IUserInfoProvider
    {
        public bool IsUserAuthenticated()
        {
            return Thread.CurrentPrincipal.Identity.IsAuthenticated;
        }

        public string GetUsername()
        {
            return Thread.CurrentPrincipal.Identity.GetUserName();
        }

        public string GetUserId()
        {
            return Thread.CurrentPrincipal.Identity.GetUserId();
        }
    }
}