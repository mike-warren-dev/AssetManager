using System.ComponentModel.DataAnnotations;

namespace AssetManager.Models;

public class Dict
{
    public Dict() 
    {
        DictOptions = new();
    }

    [Key]
    public int DictId { get; set; }
    [Required]
    public string DisplayName { get; set; }

    public List<DictOption> DictOptions { get; set; }
}
