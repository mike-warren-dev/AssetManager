using System.ComponentModel;

namespace AssetManager.DTOs;

public class PersonCreateDto
{
    [DisplayName("External ID")]
    public string ExternalId { get; set; } = null!;
    [DisplayName("First Name")]
    public string FirstName { get; set; } = null!;
    [DisplayName("Last Name")]
    public string LastName { get; set; } = null!;
    [DisplayName("Email")]
    public string Email { get; set; } = null!;
    [DisplayName("Role")]
    public int? RoleId { get; set; }
}
