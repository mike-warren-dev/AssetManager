using AssetManager.DTOs;
using AssetManager.Models;

namespace AssetManager.Services
{
    public interface IPeopleService
    {
        IEnumerable<PersonDisplayDto> GetAllPeople();
        Person GetPersonById(int id);
        PersonCreateDto GetPersonCreateDtoById(int id);
        void Create(PersonCreateDto dto);
        void Update(PersonCreateDto dto);
        void Delete(int id);
        void RemoveAssetMap(int personId, int assetId);
        void AddAssetMap(int personId, int assetId);
    }
}