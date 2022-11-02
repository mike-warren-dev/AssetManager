namespace AssetManager.DTOs;

public class PersonCreateDto
{
    public string ExternalId { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int? RoleId { get; set; }
}
