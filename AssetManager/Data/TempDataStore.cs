using AssetManager.Models;

namespace AssetManager.Data;

public class TempDataStore : IDataStore
{
    public List<Person> People { get; set; }
    public List<Asset> Assets { get; set; }

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

        Assets = new List<Asset>()
        {
            new Asset()
            {
                AssetId = 1,
                AssetType = Enums.AssetType.Laptop,
                Model = "Thinkpad X1 - Gen 8",
                Site = Enums.Site.TheWoodlands,
                PersonId = 1
            },
            new Asset()
            {
                AssetId = 2,
                AssetType = Enums.AssetType.Laptop,
                Model = "Thinkpad X1 - Gen 8",
                Site = Enums.Site.TheWoodlands,
                PersonId = 3
            },
            new Asset()
            {
                AssetId = 3,
                AssetType = Enums.AssetType.Laptop,
                Model = "Thinkpad X1 - Gen 9",
                Site = Enums.Site.TheWoodlands,
                PersonId = 4
            },
            new Asset()
            {
                AssetId = 4,
                AssetType = Enums.AssetType.Laptop,
                Model = "Thinkpad X1 - Gen 9",
                Site = Enums.Site.Remote,
                PersonId = 5
            },
            new Asset()
            {
                AssetId = 5,
                AssetType = Enums.AssetType.Laptop,
                Model = "Thinkpad X1 - Gen 9",
                Site = Enums.Site.Remote,
                PersonId = 2
            }
        };
    }
}
