using AssetManager.Models;
using AssetManager.ViewModels;

namespace AssetManager.Services;

public interface IPeopleService
{
    IEnumerable<Person> GetAllPeople();
    PersonGridViewModel GetPageOfPeople(int pageNumber);
    Person? GetPersonById(int id);
    int Create(Person person);
    void Update(Person person);
    void Delete(int id);
    void RemoveAssetMap(int personId, int assetId);
    void AddAssetMap(int personId, int assetId);
}