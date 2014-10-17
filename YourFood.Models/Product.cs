namespace YourFood.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using YourFood.Models.Enums;

    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public UnitType UnitType { get; set; }

        public int CategoryId { get; set; }

        public virtual ProductCategory Category { get; set; }
    }
}