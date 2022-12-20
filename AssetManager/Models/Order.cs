using Microsoft.EntityFrameworkCore;
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
    public int VendorId { get; set; }
    public List<ProductOrder> Products { get; set; }
    [Required]
    [Precision(18, 2)]
    public decimal Cost { get; set; }
    [Required]
    public int PurchaserId { get; set; }
    [Required]
    public int ApproverId { get; set; }
    public DateTime? ApprovedDttm { get; set; }
    public DateTime? ReceivedDttm { get; set; }

    public Person Purchaser { get; set; }
    public Person Approver { get; set; }
    public DictOption Vendor { get; set; }
}

[Owned]
public class ProductOrder
{
    [Required]
    public int ProductId { get; set; }
    [Required]
    public int? Count { get; set; }

    public DictOption Product { get; set; } = null!;
}

