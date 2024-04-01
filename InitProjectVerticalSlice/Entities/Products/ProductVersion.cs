using EventEchosAPI.Entities.Common;

namespace EventEchosAPI.Entities.Products
{
    public class ProductVersion: AuditableEntity
    {
        public int ProductId { get; set; }
        public string? Description  { get; set; }
        public string? Version { get; set; }
        public string? Price { get; set; }
        public string? Discount { get; set; }
        public bool? IsValid { get; set; }
        public bool? IsCurrent { get; set; }
        public bool? IsDeleted { get; set;}
        public Product Product  { get; set; }
        public ProductVersion()
        {
            Product = new();
        }
    }
}
