using AssetManager.DTOs;
using AssetManager.Models;
using AssetManager.ViewModels;

namespace AssetManager.Services
{
    public interface IPeopleService
    {
        IEnumerable<PersonDisplayDto> GetAllPeople();
        PersonGridViewModel GetPageOfPeople(int pageNumber);
        Person GetPersonById(int id);
        PersonAddEditDto GetPersonAddEditDtoById(int id);
        int Create(PersonAddEditDto dto);
        void Update(PersonAddEditDto dto);
        void Delete(int id);
        void RemoveAssetMap(int personId, int assetId);
        void AddAssetMap(int personId, int assetId);
    }
}