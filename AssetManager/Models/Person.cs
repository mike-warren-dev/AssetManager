using System.ComponentModel.DataAnnotations;

namespace AssetManager.Models;

public class Person
{
    public Person()
    {
        Assets = new List<Asset>();       
    }

    public int PersonId { get; set; }
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    public int? RoleId { get; set; }
  
    public List<Asset> Assets { get; set; } 
}
