using AssetManager.Models;
using AssetManager.ViewModels;

namespace AssetManager.Repos
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetAll();
        Person? GetPersonById(int id);
        int Create(Person submission);
        void Update(Person submission);
        void Delete(int id);
        void RemoveAssetMap(int personId, int assetId);
        void AddAssetMap(int personId, int assetId);
        PersonGridViewModel GetPageofPeople(int pageSize, int pageNumber);
    }
}