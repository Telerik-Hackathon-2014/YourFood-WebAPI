namespace YourFood.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class RecipeUsageRecord
    {
        [Key]
        public int Id { get; set; }

        public DateTime DateCooked { get; set; }

        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}