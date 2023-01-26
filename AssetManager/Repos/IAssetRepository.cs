using AssetManager.Models;
using AssetManager.ViewModels;

namespace AssetManager.Repos
{
    public interface IAssetRepository
    {
        List<Asset> GetAllAssets();
        AssetGridViewModel GetPageOfAssets(int pageSize, int pageNumber);
        Asset? GetAssetById(int id);
        int Create(Asset asset);
        void Delete(int id);
        void Update(Asset updatedAsset);
        void CreateAssets(IEnumerable<Asset> assets);
        List<Asset> GetAssetsByPersonId(int personId);
    }
}