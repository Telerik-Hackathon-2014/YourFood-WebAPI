namespace YourFood.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class AvailabilityProduct
    {
        [Key]
        public int Id { get; set; }

        public double Quantity { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}