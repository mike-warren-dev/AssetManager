using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManager.Models;

public class DictOption
{
    [Key]
    public int DictOptionId { get; set; }
    [Required]
    public int DictId { get; set; }
    public string DisplayName { get; set; } = null!;
    [ForeignKey("DictId")]
    public Dict Dict { get; set; } = null!;
}
