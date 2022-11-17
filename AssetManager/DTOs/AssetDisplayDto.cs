using AssetManager.Enums;

namespace AssetManager.DTOs;

public class AssetDisplayDto
{
    public int AssetId { get; set; }
    public string AssetType { get; set; } = null!;
    public string Model { get; set; } = null!;
    public string Site { get; set; } = null!;
    public int? PersonId { get; set; }
    public string PersonName { get; set; } = null!;
}
