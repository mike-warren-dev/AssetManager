using AssetManager.DTOs;
using AssetManager.Models;

namespace AssetManager.Repos
{
    public interface IPersonRepository
    {
        IEnumerable<PersonDisplayDto> GetAll();
        Person? GetPersonById(int id);
        void Create(PersonCreateDto submission);
        void Update(PersonCreateDto submission);
        void Delete(int id);
        void RemoveAsset(int personId, int assetId);
    }
}