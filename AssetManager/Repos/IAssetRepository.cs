using AssetManager.DTOs;
using AssetManager.Models;

namespace AssetManager.Repos
{
    public interface IAssetRepository
    {
        List<AssetDisplayDto> GetAll();
        Asset? GetAssetById(int id);
        void Create(AssetAddEditDto asset);
        void Delete(int id);
        void Update(AssetAddEditDto asset);
        AssetDisplayDto? GetAssetDisplayDtoById(int id);
    }
}