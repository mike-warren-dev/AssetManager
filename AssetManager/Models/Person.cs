using System.ComponentModel.DataAnnotations;

namespace AssetManager.Models;

public class Person
{
    public int PersonId { get; set; }
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public int? RoleId { get; set; }
}
