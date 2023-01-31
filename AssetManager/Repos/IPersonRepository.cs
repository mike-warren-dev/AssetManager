using AssetManager.Models;
using AssetManager.ViewModels;

namespace AssetManager.Repos;

public interface IPersonRepository
{
    Task<IEnumerable<Person>> GetAll();
    Task<Person> GetPersonById(int id);
    Task<int> Create(Person person);
    Task Update(Person updatedPerson);
    Task Delete(int id);
    Task RemoveAssetMap(int personId, int assetId);
    //Task AddAssetMap(int personId, int assetId);
    Task AddAssetMap(Person person, Asset asset);
    Task<PersonGridViewModel> GetPageofPeople(int pageSize, int pageNumber);
}