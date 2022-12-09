using AssetManager.DTOs;
using AssetManager.Models;
using AssetManager.ViewModels;

namespace AssetManager.Services
{
    public interface IAssetService
    {
        List<AssetDisplayDto> GetAllAssets();
        AssetGridViewModel GetPageOfAssets(int pageNumber);
        int Create(AssetAddEditDto asset);
        void Delete(int id);
        Asset? GetAssetById(int id);
        AssetDisplayDto? GetAssetDisplayDtoById(int id);
        void Update(AssetAddEditDto asset);
        List<AssetDisplayDto> GetAssetsByPersonId(int personId);
    }
}