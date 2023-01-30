using AssetManager.Models;
using AssetManager.ViewModels;

namespace AssetManager.Repos;

public interface IAssetRepository
{
    Task<List<Asset>> GetAllAssets();
    Task<AssetGridViewModel> GetPageOfAssets(int pageSize, int pageNumber);
    Task<Asset> GetAssetById(int id);
    Task<int> Create(Asset asset);
    Task Delete(int id);
    Task Update(Asset updatedAsset);
    Task CreateAssets(IEnumerable<Asset> assets);
    Task<List<Asset>> GetAssetsByPersonId(int personId);
}