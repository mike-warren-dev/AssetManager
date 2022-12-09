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
        PersonCreateDto GetPersonCreateDtoById(int id);
        int Create(PersonCreateDto dto);
        void Update(PersonCreateDto dto);
        void Delete(int id);
        void RemoveAssetMap(int personId, int assetId);
        void AddAssetMap(int personId, int assetId);
    }
}