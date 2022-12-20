namespace AssetManager.DTOs;

public class AssetDisplayDto
{
    public int AssetId { get; set; }
    public int AssetTypeId { get; set; }
    public string AssetType { get; set; } = null!;
    public int ModelId { get; set; }
    public string Model { get; set; } = null!;
    public int SiteId { get; set; }
    public string Site { get; set; } = null!;
    public int? PersonId { get; set; }
    public string PersonName { get; set; } = null!;
}
