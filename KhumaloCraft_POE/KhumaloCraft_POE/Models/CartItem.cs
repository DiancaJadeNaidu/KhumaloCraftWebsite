using System.ComponentModel.DataAnnotations;

namespace KhumaloCraft_Part2.Models
{
    public class CartItem
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string UserEmail { get; set; }

    }
}
