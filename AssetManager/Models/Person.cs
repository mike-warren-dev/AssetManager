namespace AssetManager.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public string ExternalId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public int? RoleId { get; set; }
    }
}
