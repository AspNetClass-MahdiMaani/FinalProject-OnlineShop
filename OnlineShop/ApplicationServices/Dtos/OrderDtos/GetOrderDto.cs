using OnlineShop.Models.DomainModels.personAggregates;

namespace OnlineShop.ApplicationServices.Dtos.OrderDtos
{
    public class GetOrderDto
    {
        public Guid? OrderHeaderId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }
        public Guid? SellerId { get; set; }
        public Guid? BuyerId { get; set; }
    }
}
