namespace OnlineShop.ApplicationServices.Dtos.ProductDtos
{
    public class UpdateProductDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public decimal? UnitPrice { get; set; }
        public string? Description { get; set; }
    }
}
