using AssetManager.DTOs;
using AssetManager.Models;
using AssetManager.ViewModels;

namespace AssetManager.Repos
{
    public interface IPersonRepository
    {
        IEnumerable<PersonDisplayDto> GetAll();
        Person? GetPersonById(int id);
        int Create(PersonAddEditDto submission);
        void Update(PersonAddEditDto submission);
        void Delete(int id);
        void RemoveAssetMap(int personId, int assetId);
        void AddAssetMap(int personId, int assetId);
        PersonGridViewModel GetPageofPeople(int pageSize, int pageNumber);
    }
}