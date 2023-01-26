using AssetManager.Models;

namespace AssetManager.ViewModels;

public class AssetGridViewModel
{
    public AssetGridViewModel() 
    {
        Assets = new();
    }

    public List<Asset> Assets { get; set; }
    public int PageCount { get; set; }
    public int CurrentPage { get; set; }
}
