using AssetManager.Enums;
using System.ComponentModel.DataAnnotations;

namespace AssetManager.DTOs;

public class AssetAddEditDto
{
    public int AssetId { get; set; }
    [Required]
    public AssetType AssetType { get; set; }
    [Required]
    public string Model { get; set; } = null!;
    [Required]
    public Site Site { get; set; }
    public int? PersonId { get; set; }
}
