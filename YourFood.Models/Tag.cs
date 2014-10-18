namespace YourFood.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class Tag
    {
        public int TagId { get; set; }

        [Required]
        public string Word { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}