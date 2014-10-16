namespace YourFood.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Recipe
    {
        private ICollection<RecipeProduct> ingredients;

        public Recipe()
        {
            this.ingredients = new HashSet<RecipeProduct>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }
      
        public double TimeToMakeInMinutes { get; private set; }

        public int CategoryId { get; set; }

        public RecipeCategory Category { get; set; }

        public virtual ICollection<RecipeProduct> Ingredients
        {
            get
            {
                return this.ingredients;
            }
            set
            {
                this.ingredients = value;
            }
        }
    }
}