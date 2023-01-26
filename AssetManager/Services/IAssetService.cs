using AssetManager.Models;
using AssetManager.ViewModels;

namespace AssetManager.Services
{
    public interface IAssetService
    {
        List<Asset> GetAllAssets();
        AssetGridViewModel GetPageOfAssets(int pageNumber);
        int Create(Asset asset);
        void Delete(int id);
        Asset? GetAssetById(int id);
        Asset? GetAssetDisplayDtoById(int id);
        void Update(Asset updatedAsset);
        List<Asset> GetAssetsByPersonId(int personId);
    }
}