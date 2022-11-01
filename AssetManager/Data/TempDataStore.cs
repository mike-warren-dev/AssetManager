using AssetManager.Models;

namespace AssetManager.Data
{
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
                    ExternalId = "bc3abb51-5d9a-4495-b11e-7ebf363edb66",
                    FirstName = "Melvin",
                    LastName = "Snyder",
                    EmailAddress = "Melvin.Snyder@SnyderCorp.com",
                    RoleId = null
                },
                new Person()
                {
                    PersonId = 2,
                    ExternalId = "efe94c20-a5b7-40eb-bd46-a201186c90b2",
                    FirstName = "Kevin",
                    LastName = "Wells",
                    EmailAddress = "Kevin.Wells@SnyderCorp.com",
                    RoleId = null
                },
                new Person()
                {
                    PersonId = 3,
                    ExternalId = "a9edb7e3-0d2a-4448-99ef-07d2abb025c6",
                    FirstName = "Martin",
                    LastName = "Hart",
                    EmailAddress = "Martin.Hart@SnyderCorp.com",
                    RoleId = null
                },
                new Person()
                {
                    PersonId = 4,
                    ExternalId = "669da6f4-c161-4f03-b33d-2d2466d4fa72",
                    FirstName = "Rose",
                    LastName = "O'Reilly",
                    EmailAddress = "Rose.OReilly@SnyderCorp.com",
                    RoleId = null
                },
               new Person()
                {
                    PersonId = 5,
                    ExternalId = "fca258f1-dee3-4b7d-a057-e37d52b5930f",
                    FirstName = "Ryan",
                    LastName = "Mills",
                    EmailAddress = "Ryan.Mills@SnyderCorp.com",
                    RoleId = null
                },
            };
        }
    }
}
