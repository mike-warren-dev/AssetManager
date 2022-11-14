using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AssetManager.Enums;

public enum Product
{
    // from 400-599
    [Display(Name = "MacBook Pro - 13\"")]
    MacbookPro13Inch = 400,
    [Display(Name = "MacBook Pro - 15\"")]
    MacbookPro15Inch = 401,







    [Display(Name = "Dell Inspiron 14")]
    DellInspiron14 = 410,








    [Display(Name = "Thinkpad E14 - Gen 3")]
    ThinkpadE14Gen3 = 420,
    [Display(Name = "Thinkpad E14 - Gen 4")]
    ThinkpadE14Gen4 = 421,


    [Display(Name = "Thinkpad X1 - Gen 8")]
    ThinkpadX1Gen8 = 425,
    [Display(Name = "Thinkpad X1 - Gen 9")]
    ThinkpadX1Gen9 = 426,

    [Display(Name = "Dell 22\" monitor - slim bezel")]
    Dell_22Inch_SE2222H = 450,

    [Display(Name = "Dell 24\" monitor - slim bezel")]
    Dell_24Inch_SE2424H = 451
}
