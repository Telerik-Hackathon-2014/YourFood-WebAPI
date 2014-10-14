namespace YourFood.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public class CatalogProduct
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Set the life time on property LifeTimePeriod
        /// </summary>
        public Int64 LifeTimeTicks { get; private set; }

        [NotMapped]
        public TimeSpan LifeTimePeriod
        {
            get
            {
                return TimeSpan.FromTicks(this.LifeTimeTicks);
            }
            set
            {
                this.LifeTimeTicks = value.Ticks;
            }
        }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}