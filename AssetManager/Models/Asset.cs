using AssetManager.Enums;
using System.ComponentModel;

namespace AssetManager.Models;

public class Asset
{
    [DisplayName("Asset ID")]
    public int AssetId { get; set; }
    [DisplayName("Asset Type")]
    public AssetType AssetType { get; set; }
    public Product Model { get; set; }
    public Site Site { get; set; }
    [DisplayName("Assigned To")]
    public int? PersonId { get; set; }

}
