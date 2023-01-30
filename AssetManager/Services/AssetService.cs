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
    
    public async Task<List<Asset>> GetAllAssets()
    {
        return await _assetRepository.GetAllAssets();
    }

    public async Task<AssetGridViewModel> GetPageOfAssets(int pageNumber)
    {
        int pageSize = 20;

        return await _assetRepository.GetPageOfAssets(pageSize, pageNumber);
    }

    public async Task<List<Asset>> GetAssetsByPersonId(int personId)
    {
        return await _assetRepository.GetAssetsByPersonId(personId);
    }

    public async Task<Asset> GetAssetById(int id)
    {
        return await _assetRepository.GetAssetById(id);
    }

    public async Task<int> Create(Asset asset)
    {
        return await _assetRepository.Create(asset);
    }
    
    public async Task Delete(int id)
    {
        await _assetRepository.Delete(id);
    }

    public async Task Update(Asset updatedAsset)
    {
        await _assetRepository.Update(updatedAsset);
    }
}
