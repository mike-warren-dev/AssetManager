using AssetManager.Enums;
using System.ComponentModel.DataAnnotations;

namespace AssetManager.Models;

public class Order
{
    public Order()
    {
        Products = new List<ProductOrder>();
    }

    public int OrderId { get; set; }
    [Required]
    public string ExternalOrderId { get; set; } = null!;
    [Required]
    public Vendor VendorId { get; set; }
    public List<ProductOrder> Products { get; set; }
    [Required]
    public Decimal Cost { get; set; }
    [Required]
    public int PurchaserId { get; set; }
    [Required]
    public int ApproverId { get; set; }
    public DateTime? ApprovedDttm { get; set; }
}

public class ProductOrder
{
    [Required]
    public Product Product { get; set; }
    [Required]
    public int? Count { get; set; }
}

