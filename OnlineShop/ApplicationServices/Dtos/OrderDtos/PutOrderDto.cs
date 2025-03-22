namespace OnlineShop.ApplicationServices.Dtos.OrderDtos
{
    public class PutOrderDto
    {
        public Guid? OrderHeaderId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }

    }
}
