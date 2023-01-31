using AssetManager.Data;
using AssetManager.Models;
using AssetManager.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AssetManager.Repos;

public class PersonRepository : IPersonRepository
{
    public readonly AssetManagerContext _context;
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
        var personCount = await _context.People.CountAsync();

        PersonGridViewModel vm = new()
        {
            CurrentPage = pageNumber,

            PageCount = (personCount + pageSize - 1) / pageSize,

            People = await _context.People.Skip((pageNumber - 1) * pageSize)
                                          .Take(pageSize)
                                          .ToListAsync()
        };

        return vm;
    }

    public async Task<Person> GetPersonById(int id)
    {
        var person = await _context.People.Include(p => p.Assets).ThenInclude(a => a.AssetType)
                                          .Include(p => p.Assets).ThenInclude(a => a.Model)
                                          .Include(p => p.Assets).ThenInclude(a => a.Site)
                                          .FirstOrDefaultAsync(p => p.PersonId == id);

        if (person == null)
            throw new ArgumentException();


        return person;
    }

    public async Task<int> Create(Person person)
    {
        _context.People.Add(person);
        
        return await _context.SaveChangesAsync();
    }

    public async Task Update(Person updatedPerson)
    {
        var person = await _context.People.FirstOrDefaultAsync(p => p.PersonId == updatedPerson.PersonId);

        if (person == null)
            throw new ArgumentException();
        

        person.FirstName = updatedPerson.FirstName;
        person.LastName = updatedPerson.LastName;
        person.Email = updatedPerson.Email;

        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var person = await _context.People.Include(p => p.Assets).FirstOrDefaultAsync(p => p.PersonId == id);

        if (person == null)
            throw new ArgumentException();
        

        foreach (var a in person.Assets)
        {
            a.PersonId = null;
        }

        _context.People.Remove(person);
        
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAssetMap(int personId, int assetId)
    {
        var person = await _context.People.Include(p => p.Assets).FirstOrDefaultAsync(p => p.PersonId == personId);

        if (person == null)
            throw new ArgumentException();

        var asset = person.Assets.FirstOrDefault(a => a.AssetId == assetId);

        if (asset == null)
            throw new ArgumentException();

        asset.PersonId = null;

        await _context.SaveChangesAsync();
    }

    public async Task AddAssetMap(Person person, Asset asset)
    {
        person.Assets.Add(asset);

        await _context.SaveChangesAsync();
    }
}
