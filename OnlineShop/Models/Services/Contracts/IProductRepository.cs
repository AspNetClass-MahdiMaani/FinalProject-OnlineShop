using OnlineShop.Frameworks.ResponseFrameworks.Contracts;
using OnlineShop.Models.DomainModels.PersonAggregates;

namespace OnlineShop.Models.Services.Contracts
{
    public interface IProductRepository: IRepository<Product, IEnumerable<Product>>
    {
    }
}
