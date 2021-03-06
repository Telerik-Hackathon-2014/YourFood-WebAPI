﻿namespace YourFood.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class ProductFrequency
    {
        [Key]
        public int ProductFrequencyId { get; set; }

        public int UsageCount { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}