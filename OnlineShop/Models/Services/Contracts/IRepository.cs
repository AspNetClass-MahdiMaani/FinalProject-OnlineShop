using Azure;
using OnlineShop.Frameworks.ResponseFrameworks.Contracts;
using OnlineShop.Models.DomainModels.ProductAggregates;

namespace OnlineShop.Models.Services.Contracts
{
    public interface IRepository<T, TCollection>
    {
        Task<IResponse<T>> InsertAsync(T obj);
        Task<IResponse<T>> UpdateAsync(T obj);
        Task<IResponse<T>> DeleteAsync(T obj);
        Task<IResponse<T>> DeleteAsync(Guid id);
        Task<IResponse<List<T>>> Select(T obj);
        Task<IResponse<T>> FindByIdAsync(Guid id);
        Task SaveChanges();
    }
}
