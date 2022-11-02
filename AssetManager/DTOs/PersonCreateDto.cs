using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AssetManager.DTOs;

public class PersonCreateDto
{
    [Required(ErrorMessage = "First Name is required")]
    [StringLength(50, ErrorMessage = "First Name is limited to 50 characters")]
    [DisplayName("First Name")]
    [DisplayName("First Name")]
    public string FirstName { get; set; } = null!;
    
    [Required(ErrorMessage = "Last Name is required")]
    [StringLength(50, ErrorMessage = "Last Name is limited to 50 characters")]
    [DisplayName("Last Name")]
    public string LastName { get; set; } = null!;
    
    [Required(ErrorMessage = "Email is required")]
    [StringLength(254, ErrorMessage = "Email is limited to 254 characters")]
    [DisplayName("Email")]
    public string Email { get; set; } = null!;
    
    [Required(ErrorMessage = "Role is required")]
    [Range(1,3, ErrorMessage = "Please select a valid Role")]
    [DisplayName("Role")]
    public int? RoleId { get; set; }
}
