using AssetManager.Data;
using AssetManager.DTOs;
using AssetManager.Models;
using AssetManager.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AssetManager.Repos;

public class PersonRepository : IPersonRepository
{
    public AssetManagerContext _context;
    public PersonRepository(AssetManagerContext context)
    {
        _context = context;
    }

    public IEnumerable<PersonDisplayDto> GetAll()
    {
        var list = _context.People.Select(p => new PersonDisplayDto {
                                    PersonId = p.PersonId,
                                    FirstName = p.FirstName,
                                    LastName = p.LastName,
                                    Email = p.Email}).ToList();

        return list;
    }

    public PersonGridViewModel GetPageofPeople(int pageSize, int pageNumber)
    {
        PersonGridViewModel vm = new();

        vm.CurrentPage = pageNumber;

        var personCount = _context.People.Count();
        vm.PageCount = (personCount + pageSize - 1) / pageSize;

        var query = _context.People.Select(p => new PersonDisplayDto
        {
            PersonId = p.PersonId,
            FirstName = p.FirstName,
            LastName = p.LastName,
            Email = p.Email
        });

        vm.People = query.Skip((pageNumber-1) * pageSize).Take(pageSize).ToList();   

        return vm;
    }

    public Person? GetPersonById(int id)
    {
        return _context.People.Include(p => p.Assets)
                                .ThenInclude(a => a.AssetType)
                              .Include(p => p.Assets)
                                .ThenInclude(a => a.Model)
                              .Include(p => p.Assets)
                                .ThenInclude(a => a.Site).FirstOrDefault(p => p.PersonId == id);
    }

    public int Create(PersonAddEditDto submission)
    {
        Person person = new()
        {
            FirstName = submission.FirstName,
            LastName = submission.LastName,
            Email = submission.Email
        };

        _context.People.Add(person);
        Save();

        return person.PersonId;
    }

    public void Update(PersonAddEditDto submission)
    {
        Person? person = _context.People.FirstOrDefault(p => p.PersonId == submission.PersonId);

        if (person != null)
        {
            person.FirstName = submission.FirstName;
            person.LastName = submission.LastName;
            person.Email = submission.Email;

            Save();
        }
    }

    public void Delete(int id)
    {
        Person? person = _context.People.Include(p => p.Assets).FirstOrDefault(p => p.PersonId == id);

        if (person != null)
        {
            foreach (var a in person.Assets)
            {
                a.PersonId = null;
            }

            _context.People.Remove(person);
            Save();
        }

    }

    public void RemoveAssetMap(int personId, int assetId)
    {
        Person? person = _context.People.Include(p => p.Assets).FirstOrDefault(p => p.PersonId == personId);

        if (person != null)
        {
            var asset = person.Assets.FirstOrDefault(a => a.AssetId == assetId);
            
            if (asset != null)
            {
                asset.PersonId = null;
                Save();
            }
        }
    }

    public void AddAssetMap(int personId, int assetId)
    {
        // IMPORTANT: This method assumes you've already done your null-checking upstream.

        var person = _context.People.Find(personId);
        var asset = _context.Assets.FirstOrDefault(a => a.AssetId == assetId);
        person.Assets.Add(asset);
        Save();
    }

    private void Save()
    {
        _context.SaveChanges();
    }
}
