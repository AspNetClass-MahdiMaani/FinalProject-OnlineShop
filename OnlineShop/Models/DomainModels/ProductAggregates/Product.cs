using OnlineShop.Frameworks;
using OnlineShop.Models.DomainModels.OrderAggregates;

namespace OnlineShop.Models.DomainModels.ProductAggregates
{
    public class Product: IDbSetEntity
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public decimal? UnitPrice { get; set; }
        public string? Description { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }
    }
}
