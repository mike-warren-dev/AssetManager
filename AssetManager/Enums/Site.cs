using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AssetManager.Enums;

public enum Site
{
    //numbered 100-199
    Remote = 199,
    
    Boston = 100,

    [Display(Name = "San Francisco")]
    SanFrancisco = 101,

    [Display(Name = "The Woodlands")]
    TheWoodlands = 102
}
