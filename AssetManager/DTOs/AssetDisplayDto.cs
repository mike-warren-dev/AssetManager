using AssetManager.Enums;

namespace AssetManager.DTOs;

public class AssetDisplayDto
{
    public int AssetId { get; set; }
    public AssetType AssetType { get; set; }
    public Product Model { get; set; }
    public Site Site { get; set; }
    public int? PersonId { get; set; }
}
