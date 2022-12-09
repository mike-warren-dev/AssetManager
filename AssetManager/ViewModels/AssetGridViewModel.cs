using AssetManager.DTOs;

namespace AssetManager.ViewModels;

public class AssetGridViewModel
{
    public AssetGridViewModel() 
    {
        Assets = new();
    }

    public List<AssetDisplayDto> Assets { get; set; }
    public int PageCount { get; set; }
    public int CurrentPage { get; set; }
}
