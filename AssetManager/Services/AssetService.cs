using AssetManager.Models;
using AssetManager.Repos;
using AssetManager.ViewModels;

namespace AssetManager.Services;

public class AssetService : IAssetService
{
    private IAssetRepository _assetRepository;
    
    public AssetService(IAssetRepository assetRepository)
    {
        _assetRepository = assetRepository;
    }
    
    public List<Asset> GetAllAssets()
    {
        return _assetRepository.GetAllAssets();
    }

    public AssetGridViewModel GetPageOfAssets(int pageNumber)
    {
        int pageSize = 20;

        return _assetRepository.GetPageOfAssets(pageSize, pageNumber);
    }

    public List<Asset> GetAssetsByPersonId(int personId)
    {
        return _assetRepository.GetAssetsByPersonId(personId);
    }

    public Asset? GetAssetById(int id)
    {
        return _assetRepository.GetAssetById(id);
    }

    public int Create(Asset asset)
    {
        return _assetRepository.Create(asset);
    }
    
    public void Delete(int id)
    {
        _assetRepository.Delete(id);
    }

    public void Update(Asset updatedAsset)
    {
        _assetRepository.Update(updatedAsset);
    }
    
    public Asset? GetAssetDisplayDtoById(int id)
    {
        return _assetRepository.GetAssetDisplayDtoById(id);
    }
}
