using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    public virtual ApplicationUser ApplicationUser { get; set; }
    public List<Asset> Assets { get; set; } 
}

public class ApplicationUser : IdentityUser
{

    public int PersonId { get; set; }
    [ForeignKey("PersonId")]
    public virtual Person Person { get; set; }
}