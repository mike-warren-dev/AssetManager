using AssetManager.DTOs;
using AssetManager.Models;

namespace AssetManager.Repos
{
    public interface IAssetRepository
    {
        List<AssetDisplayDto> GetAllAssets();
        Asset? GetAssetById(int id);
        void Create(AssetAddEditDto asset);
        void Delete(int id);
        void Update(AssetAddEditDto asset);
        AssetDisplayDto? GetAssetDisplayDtoById(int id);
        void CreateAssets(IEnumerable<AssetAddEditDto> assets);
        List<AssetDisplayDto> GetAssetsByPersonId(int personId);
    }
}