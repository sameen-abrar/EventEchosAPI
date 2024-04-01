using EventEchosAPI.Entities.Common;
using EventEchosAPI.Entities.Products;

namespace EventEchosAPI.Entities.Orders
{
    public class OrderDetail: AuditableEntity
    {
        public int OrderId  { get; set; }
        public int ProductVersionId  { get; set; }

        public ProductVersion ProductVersion { get; set; }
        public Order Order  { get; set; }

        public OrderDetail()
        {
            ProductVersion = new();
            Order = new();
        }
    }
}
