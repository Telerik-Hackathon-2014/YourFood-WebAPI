namespace YourFood.Services.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class ReceiptScannerModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ImageData { get; set; }
    }
}