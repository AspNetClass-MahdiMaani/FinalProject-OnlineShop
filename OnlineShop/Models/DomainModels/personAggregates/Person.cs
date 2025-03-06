using OnlineShop.Frameworks;

namespace OnlineShop.Models.DomainModels.personAggregates
{
    public class Person: IDbSetEntity
    {
        public Guid Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
    }
}
