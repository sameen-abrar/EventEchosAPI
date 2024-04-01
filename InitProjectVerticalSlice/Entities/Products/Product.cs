using EventEchosAPI.Entities.Common;

namespace EventEchosAPI.Entities.Products
{
    public class Product: AuditableEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public bool? IsAddon { get; set; }
        public bool IsActive { get; set; }

        public List<ProductVersion> ProductVersions { get; set; }

        public Product()
        {
            ProductVersions = [];
        }
    }
}
