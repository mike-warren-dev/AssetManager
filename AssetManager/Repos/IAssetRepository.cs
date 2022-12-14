using AssetManager.DTOs;
using AssetManager.Models;
using AssetManager.ViewModels;

namespace AssetManager.Repos
{
    public interface IAssetRepository
    {
        List<AssetDisplayDto> GetAllAssets();
        AssetGridViewModel GetPageOfAssets(int pageSize, int pageNumber);
        Asset? GetAssetById(int id);
        int Create(AssetAddEditDto asset);
        void Delete(int id);
        void Update(AssetAddEditDto asset);
        AssetDisplayDto? GetAssetDisplayDtoById(int id);
        void CreateAssets(IEnumerable<AssetAddEditDto> assets);
        List<AssetDisplayDto> GetAssetsByPersonId(int personId);
    }
}