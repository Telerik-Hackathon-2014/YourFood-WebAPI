namespace YourFood.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class ShoppingList
    {
        private ICollection<RecipeProduct> products;

        public ShoppingList()
        {
            this.products = new HashSet<RecipeProduct>();
        }

        [Key]
        public int Id { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateFinished { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public virtual ICollection<RecipeProduct> Products
        {
            get
            {
                return this.products;
            }
            set
            {
                this.products = value;
            }
        }
    }
}