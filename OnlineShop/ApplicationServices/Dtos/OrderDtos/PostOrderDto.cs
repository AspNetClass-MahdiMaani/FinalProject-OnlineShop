namespace OnlineShop.ApplicationServices.Dtos.OrderDtos
{
    public class PostOrderDto
    {
        public Guid ProductId { get; set; }
        public Guid? SellerId { get; set; }
        public Guid? BuyerId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }

    }
}
