using System.ComponentModel.DataAnnotations;

namespace KhumaloCraft_Part2.Models
{
    public class Products
    {
        [Key]
        public int ProductID { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Category { get; set; }
        public bool Availability { get; set; }
        public string? ImageUrl { get; set; }
    }
}
