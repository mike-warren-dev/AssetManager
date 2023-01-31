using AssetManager.Models;
using AssetManager.Repos;
using AssetManager.ViewModels;

namespace AssetManager.Services;

public class PeopleService : IPeopleService
{
    private readonly IPersonRepository _personRepository;
    private readonly IAssetRepository _assetRepository;

    public PeopleService(IPersonRepository personRepository, IAssetRepository assetRepository)
    {
        _personRepository = personRepository;
        _assetRepository = assetRepository;
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
        if (id == 0)
            throw new ArgumentException();

        return await _personRepository.GetPersonById(id);
    }

    public async Task<int> Create(Person person)
    {
        return await _personRepository.Create(person);
    }

    public async Task Update(Person person)
    {
        await _personRepository.Update(person);
    }

    public async Task Delete(int id)
    {
        await _personRepository.Delete(id);
    }

    public async Task RemoveAssetMap(int personId, int assetId)
    {
        await _personRepository.RemoveAssetMap(personId, assetId);
    }

    public async Task<Asset> MapAssetToPerson(int personId, int assetId)
    {
        var person = await GetPersonById(personId);

        var asset = await _assetRepository.GetAssetById(assetId);

        if (asset.PersonId != null)
        {
            if (person.Assets.Contains(asset))
                throw new ArgumentException("The Asset is already mapped to this user.");

            try
            {
                var otherPerson = await GetPersonById((int)asset.PersonId);

                throw new ArgumentException($"The Asset is already mapped to {otherPerson.FirstName} {otherPerson.LastName}");
            }
            catch
            {
                throw new ArgumentException("The Asset is already mapped.");
            }
        }

        
        await _personRepository.AddAssetMap(person, asset);

        // Doing this to avoid a circular reference when serializing asset downstream. Will fix. 
        asset.Person = null;

        return asset;
    }
}
