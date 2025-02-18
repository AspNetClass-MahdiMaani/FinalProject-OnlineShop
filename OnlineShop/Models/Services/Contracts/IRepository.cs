using Azure;
using OnlineShop.Frameworks.ResponseFrameworks.Contracts;
using OnlineShop.Models.DomainModels.PersonAggregates;

namespace OnlineShop.Models.Services.Contracts
{
    public interface IRepository<T, TCollection>
    {
        Task<IResponse<T>> InsertAsync(T obj);
        Task<IResponse<T>> UpdateAsync(T obj);
        Task<IResponse<T>> DeleteAsync(T obj);
        Task<IResponse<T>> DeleteAsync(Guid Id);
        Task<IResponse<List<T>>> Select(T obj);
        Task<IResponse<Product>> FindByIdAsync(Guid id);
        Task SaveChanges();
    }
}
