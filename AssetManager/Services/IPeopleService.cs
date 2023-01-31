using AssetManager.Models;
using AssetManager.ViewModels;

namespace AssetManager.Services;

public interface IPeopleService
{
    Task<IEnumerable<Person>> GetAllPeople();
    Task<PersonGridViewModel> GetPageOfPeople(int pageNumber);
    Task<Person> GetPersonById(int id);
    Task<int> Create(Person person);
    Task Update(Person person);
    Task Delete(int id);
    Task RemoveAssetMap(int personId, int assetId);
    Task<Asset> MapAssetToPerson(int personId, int assetId);
}