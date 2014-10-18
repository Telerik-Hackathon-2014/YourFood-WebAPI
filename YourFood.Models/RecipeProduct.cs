namespace YourFood.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using YourFood.Models.Enums;

    public class RecipeProduct
    {
        private ICollection<Recipe> recipes;

        public RecipeProduct()
        {
            this.recipes = new HashSet<Recipe>();
        }

        [Key]
        public int Id { get; set; }

        public double Quantity { get; set; }

        public UnitType UnitType { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int ShoppingListId { get; set; }

        public virtual ShoppingList ShoppingList { get; set; }

        public virtual ICollection<Recipe> Recipes
        {
            get
            {
                return this.recipes;
            }
            set
            {
                this.recipes = value;
            }
        }
    }
}