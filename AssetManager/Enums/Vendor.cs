using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AssetManager.Enums;

public enum Vendor
{
    // from 300-399
    WorkQuest = 300,
    [Display(Name = "Texas SmartBuy")]
    TxSmartBuy = 301,
    CDW = 302,
    Walmart = 303,
    Amazon = 304
}
