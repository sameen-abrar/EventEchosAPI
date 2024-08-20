using System.Reflection.Metadata.Ecma335;

namespace EventEchosAPI.Entities
{
    public class Order : AuditableEntity
    {
        public int? CouponId { get; set; }
        public int CoordinatorId { get; set; }
        public string? Description { get; set; }
        public int? Status { get; set; }

        public Coupon Coupon { get; set; }
        public Coordinator Coordinator { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
        public List<Transaction> Transactions { get; set; }

        public Order()
        {
            Coupon = new();
            Coordinator = new();
            OrderDetails = [];
            Transactions = [];
        }
    }
}
