namespace YourFood.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class CatalogProduct
    {
        [Key]
        public int Id { get; set; }
        
        public int LifetimeInDays { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}