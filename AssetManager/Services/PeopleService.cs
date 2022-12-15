using AssetManager.DTOs;
using AssetManager.Models;
using AssetManager.Repos;
using AssetManager.ViewModels;
using NuGet.Protocol.Core.Types;

namespace AssetManager.Services;

public class PeopleService : IPeopleService
{
    private IPersonRepository _personRepository;

    public PeopleService(IPersonRepository repository)
    {
        _personRepository = repository;
    }

    public IEnumerable<PersonDisplayDto> GetAllPeople()
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
        return _personRepository.GetPersonById(id);
    }

    public PersonAddEditDto GetPersonAddEditDtoById(int id)
    {
        PersonAddEditDto dto = new();

        Person person = _personRepository.GetPersonById(id);

        dto.PersonId = person.PersonId;
        dto.FirstName = person.FirstName;
        dto.LastName = person.LastName;
        dto.Email = person.Email;
        dto.RoleId = person.RoleId;
        dto.Assets = person.Assets;

        return dto;

    }

    public int Create(PersonAddEditDto dto)
    {
        return _personRepository.Create(dto);
    }

    public void Update(PersonAddEditDto dto)
    {
        _personRepository.Update(dto);
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
