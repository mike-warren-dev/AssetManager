using System.ComponentModel;

namespace AssetManager.Models;

public class Asset
{
    [DisplayName("Asset ID")]
    public int AssetId { get; set; }
    [DisplayName("Asset Type")]
    //[Required]
    public int AssetTypeId { get; set; }
    public int ModelId { get; set; }
    public int SiteId { get; set; }
    [DisplayName("Assigned To")]
    public int? PersonId { get; set; }
    public Person? Person { get; set; }
    //[ForeignKey("AssetTypeId")]
    public DictOption AssetType { get; set; } = null!;
    public DictOption Model { get; set; } = null!;
    public DictOption Site { get; set; } = null!;
}
