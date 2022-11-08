﻿using AssetManager.Data;
using AssetManager.DTOs;
using AssetManager.Models;

namespace AssetManager.Repos;

public class PersonRepository : IPersonRepository
{
    public IDataStore _context;

    public PersonRepository(IDataStore context)
    {
        _context = context;
    }

    public IEnumerable<PersonDisplayDto> GetAll()
    {
        var list = _context.People.Select(p => new PersonDisplayDto {
                                    PersonId = p.PersonId,
                                    FirstName = p.FirstName,
                                    LastName = p.LastName,
                                    Email = p.Email,
                                    RoleId = p.RoleId}).ToList();

        return list;
    }
    public Person? GetPersonById(int id)
    {
        return _context.People.FirstOrDefault(p => p.PersonId == id); ;
    }
    public void Create(PersonCreateDto submission)
    {
        Person person = new()
        {
            PersonId = _context.People.Max(p => p.PersonId) + 1,
            FirstName = submission.FirstName,
            LastName = submission.LastName,
            Email = submission.Email,
            RoleId = submission.RoleId
        };

        _context.People.Add(person);
    }

    public void Update(PersonCreateDto submission)
    {
        Person? person = _context.People.FirstOrDefault(p => p.PersonId == submission.PersonId);

        if (person != null)
        {
            person.PersonId = submission.PersonId;
            person.FirstName = submission.FirstName;
            person.LastName = submission.LastName;
            person.Email = submission.Email;
            person.RoleId = submission.RoleId;

            int index = _context.People.FindIndex(p => p.PersonId == person.PersonId);

            _context.People[index] = person;
        }
    }

    public void Delete(int id)
    {
        Person? person = _context.People.FirstOrDefault(p => p.PersonId == id);
        
        if (person != null)
            _context.People.Remove(person); 
    }
}
