using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AssetManager.DTOs;

public class AssetAddEditDto
{
    public int AssetId { get; set; }
    [Required]
    [DisplayName("Asset Type")]
    public int AssetTypeId { get; set; }
    [Required]
    public int ModelId { get; set; }
    [Required]
    public int SiteId { get; set; }
    [DisplayName("Assigned To")]
    public int? PersonId { get; set; }
    public string PersonName { get; set; } = null!;

}
