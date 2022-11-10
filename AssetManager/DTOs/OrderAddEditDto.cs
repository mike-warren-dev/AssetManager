using AssetManager.Enums;
using AssetManager.Models;
using System.ComponentModel.DataAnnotations;

namespace AssetManager.DTOs;

public class OrderAddEditDto
{
    //public OrderAddEditDto()
    //{
    //    Products = new List<ProductOrder>() {
    //        new ProductOrder(),
    //        new ProductOrder(),
    //        new ProductOrder(),
    //        new ProductOrder(),
    //        new ProductOrder(),
    //        new ProductOrder()};
    //}
    public OrderAddEditDto()
    {
        Products = new List<ProductOrder>() { };
    }

    public int? OrderId { get; set; }
    [Required]
    public string ExternalOrderId { get; set; } = null!;
    [Required]
    public Vendor VendorId { get; set; }
    public List<ProductOrder> Products { get; set; } = null!;
    [Required]
    public Decimal? Cost { get; set; }
    [Required]
    public int? PurchaserId { get; set; }
    [Required]
    public int? ApproverId { get; set; }
    public DateTime? ApprovedDttm { get; set; }
}
