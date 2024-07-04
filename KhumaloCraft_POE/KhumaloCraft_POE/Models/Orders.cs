using KhumaloCraft_Part2.Models;
using System.ComponentModel.DataAnnotations;

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
    public string? ProductName { get; set; } //added new property
    public Products? Product { get; set; }
}
