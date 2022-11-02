using AssetManager.Models;

namespace AssetManager.Data;

public class TempDataStore : IDataStore
{
    public List<Person> People { get; set; }

    public static TempDataStore Data { get; } = new TempDataStore();

    public TempDataStore()
    {
        People = new List<Person>()
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
    }
}
