using OnlineShop.Frameworks;
using OnlineShop.Models.DomainModels.personAggregates;

namespace OnlineShop.Models.DomainModels.OrderAggregates
{
    public class OrderHeader : IDbSetEntity
    {
        public Guid Id { get; set; }
        public Guid? SallerId { get; set; }
        public Person Seller { get; set; }
        public Guid? BuyerId { get; set; }
        public Person Buyer { get; set; }
        public List<OrderDetail>? OrderDetail { get; set; }
    }
}
