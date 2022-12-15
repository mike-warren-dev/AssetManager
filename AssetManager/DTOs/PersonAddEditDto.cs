using AssetManager.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AssetManager.DTOs;

public class PersonAddEditDto
{
    public PersonAddEditDto()
    {
        Assets = new List<Asset>();
    }

    public int? PersonId { get; set; } = 0;

    [Required(ErrorMessage = "First Name is required")]
    [StringLength(50, ErrorMessage = "First Name is limited to 50 characters")]
    [DisplayName("First Name")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Last Name is required")]
    [StringLength(50, ErrorMessage = "Last Name is limited to 50 characters")]
    [DisplayName("Last Name")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Email is required")]
    [StringLength(254, ErrorMessage = "Email is limited to 254 characters")]
    [EmailAddress]
    [DisplayName("Email")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Role is required")]
    [Range(200, 299, ErrorMessage = "Please select a valid Role")]
    [DisplayName("Role")]
    public int? RoleId { get; set; }

    public List<Asset> Assets { get; set; } = null!;
}
