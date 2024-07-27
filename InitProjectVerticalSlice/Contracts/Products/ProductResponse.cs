
namespace EventEchosAPI.Contracts.Products
{
    public class ProductResponse
    {
        public List<ProductModel> Products { get; set; }

        public ProductResponse() {
            Products = [];
        }
    }

    public class ProductModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        public List<ProductVersionModel> ProductVersions { get; set; }

        public ProductModel()
        {
            ProductVersions = [];
        }
    }

    public class ProductVersionModel
    {
        public string Description { get; set; }
        public string Version { get; set; }
        public string Price { get; set; }
        public string Discount { get; set; }
    }

}
