using System.ComponentModel.DataAnnotations;

namespace VillanyszamlakoltsegApi.Models
{
    public class ElectricityBill
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        public int[] Years { get; set; }

        public decimal[,] Consumptions { get; set; }
    }
}
