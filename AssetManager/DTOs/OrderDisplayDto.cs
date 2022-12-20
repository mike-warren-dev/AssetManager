using AssetManager.Models;

namespace AssetManager.DTOs;

public class OrderDisplayDto
{
    public int OrderId { get; set; }
    public string ExternalOrderId { get; set; } = null!;
    public int VendorId { get; set; }
    public string Vendor { get; set; } = null!;
    public List<ProductOrder> Products { get; set; } = null!;
    public Decimal Cost { get; set; }
    public int PurchaserId { get; set; }
    public string PurchaserName { get; set; } = null!;
    public int ApproverId { get; set; }
    public string ApproverName { get; set; } = null!;
    public DateTime? ApprovedDttm { get; set; }
    public DateTime? ReceivedDttm { get; set; }
}
