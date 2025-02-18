
using System.Net;
using System;
using OnlineShop.Frameworks.ResponseFrameworks.Contracts;
using OnlineShop.Models.DomainModels.PersonAggregates;
using OnlineShop.Models.Services.Contracts;
using OnlineShop.Frameworks.ResponseFrameworks;
using OnlineShop.Frameworks;
using Microsoft.EntityFrameworkCore;

namespace OnlineShop.Models.Services.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataBaseContext _context;

        #region [-Ctor-]

        public ProductRepository(DataBaseContext context)
        {
                _context = context;
        }
        #endregion

        #region [- DeleteAsync(Product obj)-]

        public async Task<IResponse<Product>> DeleteAsync(Product obj)
        {

            if (_context.Entry(obj).State == EntityState.Detached)
            {
                _context.Product.Attach(obj);
            }
            _context.Remove(obj);
            await SaveChanges();
            return new Response<Product>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, obj);

        }

        #endregion

        #region [- DeleteAsync(Guid Id)-]

        public async Task<IResponse<Product>> DeleteAsync(Guid Id)
        {
            var entityToDelete = _context.Product.Find(Id);
            _context.Remove(entityToDelete);
            await SaveChanges();
            return new Response<Product>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, null);
        }
        #endregion

        #region [- FindByIdAsync(Guid id)-]
        public async Task<IResponse<Product>> FindByIdAsync(Guid id)
        {
            var q = _context.Product.FindAsync(id);
            return (IResponse<Product>) await q;
        }
        #endregion

        #region [- InsertAsync(Product obj)-]

        public async Task<IResponse<Product>> InsertAsync(Product obj)
        {
            using (_context)
            {
                try
                {
                    if (obj is null)
                    {
                        return new Response<Product>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                    }
                    _context.Product.Add(obj);
                    await SaveChanges();
                    return new Response<Product>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, obj);
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }

        #endregion

        #region [- SaveChanges()-]

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        #endregion

        #region [- Select(Product obj)-]

        public async Task <IResponse<List<Product>>> Select(Product obj)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region [- UpdateAsync(Product obj)-]

        public Task<IResponse<Product>> UpdateAsync(Product obj)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}