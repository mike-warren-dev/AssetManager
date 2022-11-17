namespace AssetManager.Models
{
    public class AssetType_shelved
    {
        public AssetType_shelved()
        {
            Products = new List<Product_shelved>();
        }

        public int AssetTypeId { get; set; }
        public string DisplayName { get; set; } = null!;
        public List<Product_shelved> Products { get; set; }
    }
}
