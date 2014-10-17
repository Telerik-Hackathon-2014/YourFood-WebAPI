namespace YourFood.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class AvailabilityProduct
    {
        [Key]
        public int Id { get; set; }

        public double InitialQuantity { get; set; }

        public double CurrentQuantity { get; set; }

        public DateTime? DateAdded { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}