using AssetManager.DTOs;
using AssetManager.Models;

namespace AssetManager.Services
{
    public interface IAssetService
    {
        List<AssetDisplayDto> GetAllAssets();
        void Create(AssetAddEditDto asset);
        void Delete(int id);
        Asset? GetAssetById(int id);
        AssetDisplayDto? GetAssetDisplayDtoById(int id);
        void Update(AssetAddEditDto asset);
    }
}