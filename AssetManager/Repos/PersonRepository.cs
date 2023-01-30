using AssetManager.Data;
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

    public async Task<IEnumerable<Person>> GetAll()
    {
        return await _context.People.ToListAsync();
    }

    public async Task<PersonGridViewModel> GetPageofPeople(int pageSize, int pageNumber)
    {
        PersonGridViewModel vm = new();

        vm.CurrentPage = pageNumber;

        var personCount = await _context.People.CountAsync();
        vm.PageCount = (personCount + pageSize - 1) / pageSize; 

        vm.People = await _context.People.Skip((pageNumber - 1) * pageSize)
                                         .Take(pageSize)
                                         .ToListAsync();

        return vm;
    }

    public async Task<Person> GetPersonById(int id)
    {
        var person = await _context.People.Include(p => p.Assets).ThenInclude(a => a.AssetType)
                                          .Include(p => p.Assets).ThenInclude(a => a.Model)
                                          .Include(p => p.Assets).ThenInclude(a => a.Site)
                                          .FirstOrDefaultAsync(p => p.PersonId == id);

        if (person != null) 
        {
            return person;
        }
        else
        {
            throw new ArgumentException();
        }
    }

    public async Task<int> Create(Person person)
    {
        _context.People.Add(person);
        
        await _context.SaveChangesAsync();

        return person.PersonId;
    }

    public async Task Update(Person updatedPerson)
    {
        var person = await _context.People.FirstOrDefaultAsync(p => p.PersonId == updatedPerson.PersonId);

        if (person != null)
        {
            person.FirstName = updatedPerson.FirstName;
            person.LastName = updatedPerson.LastName;
            person.Email = updatedPerson.Email;

            await _context.SaveChangesAsync();
        }
    }

    public async Task Delete(int id)
    {
        var person = await _context.People.Include(p => p.Assets).FirstOrDefaultAsync(p => p.PersonId == id);

        if (person != null)
        {
            foreach (var a in person.Assets)
            {
                a.PersonId = null;
            }

            _context.People.Remove(person);
            await _context.SaveChangesAsync();
        }

    }

    public async Task RemoveAssetMap(int personId, int assetId)
    {
        var person = await _context.People.Include(p => p.Assets).FirstOrDefaultAsync(p => p.PersonId == personId);

        if (person != null)
        {
            var asset = person.Assets.FirstOrDefault(a => a.AssetId == assetId);
            
            if (asset != null)
            {
                asset.PersonId = null;
                await _context.SaveChangesAsync();
            }
        }
    }

    public async Task AddAssetMap(int personId, int assetId)
    {
        var person = await _context.People.FirstOrDefaultAsync(p => p.PersonId == personId);
        var asset = await _context.Assets.FirstOrDefaultAsync(a => a.AssetId == assetId);
        
        if (asset != null && person != null)
        {
            person.Assets.Add(asset);

            await _context.SaveChangesAsync();
        }
        else
        {
            throw new ArgumentException();
        }        
    }
}
