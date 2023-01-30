using AssetManager.Models;
using AssetManager.ViewModels;

namespace AssetManager.Services;

public interface IAssetService
{
    Task<List<Asset>> GetAllAssets();
    Task<AssetGridViewModel> GetPageOfAssets(int pageNumber);
    Task<int> Create(Asset asset);
    Task Delete(int id);
    Task<Asset> GetAssetById(int id);
    Task Update(Asset updatedAsset);
    Task<List<Asset>> GetAssetsByPersonId(int personId);
}