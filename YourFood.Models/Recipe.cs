namespace YourFood.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

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

        /// <summary>
        /// Set the time to make on property TimeToMakePeriod
        /// </summary>
        public Int64 TimeToMakeTicks { get; private set; }

        [NotMapped]
        public TimeSpan TimeToMakePeriod
        {
            get
            {
                return TimeSpan.FromTicks(this.TimeToMakeTicks);
            }
            set
            {
                this.TimeToMakeTicks = value.Ticks;
            }
        }

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