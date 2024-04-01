using EventEchosAPI.Entities.Common;

namespace EventEchosAPI.Entities.Orders
{
    public class Transaction: AuditableEntity
    {
        public int? Status { get; set; }
        public int OrderId { get; set; }
        public bool? IsComplete { get; set; }

        public Order Order { get; set; }

        public Transaction()
        {
            Order = new();
        }
    }
}
