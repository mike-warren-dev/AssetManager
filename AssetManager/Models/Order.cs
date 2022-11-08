using AssetManager.Enums;

namespace AssetManager.Models;

public class Order
{
    public Order()
    {
        Products = new List<Product>();
    }

    public int OrderId { get; set; }
    public string ExternalOrderId { get; set; } = null!;
    public Vendor VendorId { get; set; }
    public List<Product> Products { get; set; }
    public Decimal Cost { get; set; }
    public int PurchaserId { get; set; }
    public int ApproverId { get; set; }
    public DateTime? ApprovedDttm { get; set; }
}
