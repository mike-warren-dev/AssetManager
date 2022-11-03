using Microsoft.EntityFrameworkCore;
using AssetManager.Models;

namespace AssetManager.Data;

public class AssetManagerContext : DbContext
{
    public DbSet<Person> People { get; set; } = null!;

    public AssetManagerContext()
    {        
        List<Person> list = new List<Person>()
        {
            new Person()
            {
                PersonId = 1,
                FirstName = "Melvin",
                LastName = "Snyder",
                Email = "Melvin.Snyder@SnyderCorp.com",
                RoleId = null
            },
            new Person()
            {
                PersonId = 2,
                FirstName = "Kevin",
                LastName = "Wells",
                Email = "Kevin.Wells@SnyderCorp.com",
                RoleId = null
            },
            new Person()
            {
                PersonId = 3,
                FirstName = "Martin",
                LastName = "Hart",
                Email = "Martin.Hart@SnyderCorp.com",
                RoleId = null
            },
            new Person()
            {
                PersonId = 4,
                FirstName = "Rose",
                LastName = "O'Reilly",
                Email = "Rose.OReilly@SnyderCorp.com",
                RoleId = null
            },
           new Person()
            {
                PersonId = 5,
                FirstName = "Ryan",
                LastName = "Mills",
                Email = "Ryan.Mills@SnyderCorp.com",
                RoleId = null
            },
        };

        People.AddRange(list);
    }
}
