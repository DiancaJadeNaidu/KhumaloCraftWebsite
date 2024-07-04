using System.ComponentModel.DataAnnotations;

namespace KhumaloCraft_Part2.Models
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public string? Status { get; set; }
        [Required]
        [MaxLength(256)]
        public string? UserEmail { get; set; }
        [Required]
        [MaxLength(256)]
        public string? UserId { get; set; }
        public string? ProductName { get; set; }
        public Products? Product { get; set; }
    }

    public class OrderHistoryViewModel
    {
        public int OrderId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public string? Status { get; set; }
        public string? UserEmail { get; set; }
    }
}
