namespace OnlineShop.Models.DomainModels.PersonAggregates
{
    public class Product
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public decimal? UnitPrice { get; set; }
        public string? Description { get; set; }
    }
}
