namespace YourFood.Models
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        private ICollection<RecipeUsageRecord> recipeUsageRecords;
        private ICollection<ShoppingList> shoppingLists;

        public User()
        {
            this.recipeUsageRecords = new HashSet<RecipeUsageRecord>();
            this.shoppingLists = new HashSet<ShoppingList>();
        }

        public virtual ICollection<ShoppingList> ShoppingLists
        {
            get
            {
                return this.shoppingLists;
            }
            set
            {
                this.shoppingLists = value;
            }
        }

        public virtual ICollection<RecipeUsageRecord> RecipeUsageRecords
        {
            get
            {
                return this.recipeUsageRecords;
            }
            set
            {
                this.recipeUsageRecords = value;
            }
        }
        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            return userIdentity;
        }
    }
}