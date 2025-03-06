using OnlineShop.Frameworks;
using OnlineShop.Models.DomainModels.ProductAggregates;

namespace OnlineShop.Models.DomainModels.OrderAggregates
{
    public class OrderDetail : IDbSetEntity
    {
        public Guid? OrderHeaderId { get; set; }
        public Guid? ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }
        public OrderHeader? OrderHeader { get; set; }
        public Product? Product { get; set; }
    }
}
