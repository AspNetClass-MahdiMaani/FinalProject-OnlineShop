using OnlineShop.Frameworks;

namespace OnlineShop.Models.DomainModels.personAggregates
{
    public class Person: IDbSetEntity
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
