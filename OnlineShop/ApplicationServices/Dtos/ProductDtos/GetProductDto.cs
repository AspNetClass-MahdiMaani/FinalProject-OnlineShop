namespace OnlineShop.ApplicationServices.Dtos.ProductDtos
{
    public class GetProductDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public decimal? UnitPrice { get; set; }
        public string? Description { get; set; }
    }
}
