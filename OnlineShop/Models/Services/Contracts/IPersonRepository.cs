using OnlineShop.Models.DomainModels.personAggregates;

namespace OnlineShop.Models.Services.Contracts
{
    public interface IPersonRepository:IRepository<Person, IEnumerable<Person>>
    {
    }
}
