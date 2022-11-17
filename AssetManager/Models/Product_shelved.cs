using AssetManager.Enums;

namespace AssetManager.Models
{
    public class Product_shelved
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public AssetType_shelved AssetType { get; set; }
    }
}
