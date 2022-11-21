using System.ComponentModel.DataAnnotations;

namespace AssetManager.Enums;

public enum AssetType
{
    //numbered 0-99
    Dock = 1,
    Laptop = 2,
    Desktop = 3,
    Monitor = 4,

    [Display(Name = "Desk Phone")]
    DeskPhone = 5,

    [Display(Name = "Mobile Phone")]
    MobilePhone =6
}
