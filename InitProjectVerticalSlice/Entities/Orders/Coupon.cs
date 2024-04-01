using EventEchosAPI.Entities.Common;

namespace EventEchosAPI.Entities.Orders
{
    public class Coupon : AuditableEntity
    {
        public DateTime ExpiryDate { get; set; }
        public string Code  { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountLimit { get; set; }
    }
}
