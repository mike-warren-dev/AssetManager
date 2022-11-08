using AssetManager.Enums;

namespace AssetManager.DTOs;

public class OrderDisplayDto
{
    public int OrderId { get; set; }
    public string ExternalOrderId { get; set; } = null!;
    public Vendor VendorId { get; set; }
    public List<Product> Products { get; set; } = null!;
    public Decimal Cost { get; set; }
    public int PurchaserId { get; set; }
    public int ApproverId { get; set; }
    public DateTime? ApprovedDttm { get; set; }
}
