namespace YourFood.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

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

        public int ProductId { get; set; }

        public Product Product { get; set; }

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