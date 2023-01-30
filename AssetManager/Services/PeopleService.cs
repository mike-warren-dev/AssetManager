using AssetManager.Models;
using AssetManager.Repos;
using AssetManager.ViewModels;

namespace AssetManager.Services;

public class PeopleService : IPeopleService
{
    private IPersonRepository _personRepository;

    public PeopleService(IPersonRepository repository)
    {
        _personRepository = repository;
    }

    public async Task<IEnumerable<Person>> GetAllPeople()
    {
        return await _personRepository.GetAll();
    }

    public async Task<PersonGridViewModel> GetPageOfPeople(int pageNumber)
    {
        int pageSize = 20;

        return await _personRepository.GetPageofPeople(pageSize, pageNumber);
    }

    public async Task<Person> GetPersonById(int id)
    {
        return await _personRepository.GetPersonById(id);
    }

    public async Task<int> Create(Person person)
    {
        return await _personRepository.Create(person);
    }

    public Task Update(Person person)
    {
        return _personRepository.Update(person);
    }

    public async Task Delete(int id)
    {
        await _personRepository.Delete(id);
    }

    public Task RemoveAssetMap(int personId, int assetId)
    {
        return _personRepository.RemoveAssetMap(personId, assetId);
    }

    public Task AddAssetMap(int personId, int assetId)
    {
        return _personRepository.AddAssetMap(personId, assetId);
    }

}
