using AssetManager.DTOs;
using AssetManager.Models;
using AssetManager.Repos;
using AssetManager.ViewModels;
using NuGet.ContentModel;
using NuGet.Protocol.Core.Types;

namespace AssetManager.Services;

public class PeopleService : IPeopleService
{
    private IPersonRepository _personRepository;

    public PeopleService(IPersonRepository repository)
    {
        _personRepository = repository;
    }

    public IEnumerable<Person> GetAllPeople()
    {
        return _personRepository.GetAll();
    }

    public PersonGridViewModel GetPageOfPeople(int pageNumber)
    {
        int pageSize = 20;

        return _personRepository.GetPageofPeople(pageSize, pageNumber);
    }

    public Person GetPersonById(int id)
    {
        Person? person = _personRepository.GetPersonById(id);

        if (person == null)
        {
            return new Person();
        }
        else
        {
            return person;
        }
    }

    public Person GetPersonAddEditDtoById(int id)
    {
        Person person = _personRepository.GetPersonById(id);

        if (person == null) 
        { 
            return new Person();
        }
        else
        {
            return person;
        }
        //person.PersonId = person.PersonId;
        //person.FirstName = person.FirstName;
        //person.LastName = person.LastName;
        //person.Email = person.Email;
        //person.Assets = person.Assets.Select(a => new AssetDisplayDto()
        //{
        //    AssetId = a.AssetId,
        //    AssetTypeId = a.AssetTypeId,
        //    AssetType = a.AssetType.DisplayName,
        //    ModelId= a.ModelId,
        //    Model = a.Model.DisplayName,
        //    SiteId = a.SiteId,
        //    Site = a.Site.DisplayName,
        //    PersonId = a.PersonId
        //}).ToList();

        //return person;

    }

    public int Create(Person person)
    {
        return _personRepository.Create(person);
    }

    public void Update(Person person)
    {
        _personRepository.Update(person);
    }

    public void Delete(int id)
    {
        _personRepository.Delete(id);
    }

    public void RemoveAssetMap(int personId, int assetId)
    {
        _personRepository.RemoveAssetMap(personId, assetId);
    }

    public void AddAssetMap(int personId, int assetId)
    {
        _personRepository.AddAssetMap(personId, assetId);
    }

}
