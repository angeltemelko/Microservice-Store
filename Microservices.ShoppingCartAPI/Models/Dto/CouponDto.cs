namespace Microservices.Services.CouponAPI.Models
{
    public class CouponDto
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; } = string.Empty;
        public int DiscountAmount { get; set; }
        public int MinAmount { get; set; }
        public DateTime LatUpdated { get; set; }
    }
}
