using AssetManager.Enums;
using System.ComponentModel.DataAnnotations;

namespace AssetManager.Models;

public class Order
{
    public Order()
    {
        Products = new List<Product>();
    }

    public int OrderId { get; set; }
    [Required]
    public string ExternalOrderId { get; set; } = null!;
    [Required]
    public Vendor VendorId { get; set; }
    public List<Product> Products { get; set; }
    [Required]
    public Decimal Cost { get; set; }
    [Required]
    public int PurchaserId { get; set; }
    [Required]
    public int ApproverId { get; set; }
    public DateTime? ApprovedDttm { get; set; }
}
