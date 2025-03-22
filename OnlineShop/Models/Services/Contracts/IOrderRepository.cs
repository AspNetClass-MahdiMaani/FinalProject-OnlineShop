using OnlineShop.Frameworks.ResponseFrameworks.Contracts;
using OnlineShop.Models.DomainModels.OrderAggregates;


namespace OnlineShop.Models.Services.Contracts
{
    public interface IOrderRepository: IRepository<OrderHeader, IEnumerable<OrderHeader>>
    {
        
    }
}
