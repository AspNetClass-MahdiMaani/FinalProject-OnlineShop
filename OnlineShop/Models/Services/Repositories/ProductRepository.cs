using System.Net;
using System;
using OnlineShop.Frameworks.ResponseFrameworks.Contracts;
using OnlineShop.Models.DomainModels.ProductAggregates;
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
            try
            {
                if (_context.Entry(obj).State == EntityState.Detached)
                {
                    _context.Product.Attach(obj);
                }
                _context.Remove(obj);
                await SaveChanges();
                return new Response<Product>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, obj);
            }
            catch (Exception)
            {

                return new Response<Product>(false, HttpStatusCode.InternalServerError, ResponseMessages.Error, null);
            }
        }

        #endregion

        #region [- DeleteAsync(Guid Id)-]

        public async Task<IResponse<Product>> DeleteAsync(Guid id)
        {
            try
            {
                var entityToDelete = _context.Product.Find(id);
                _context.Remove(entityToDelete);
                await SaveChanges();
                return new Response<Product>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, null);
            }
            catch (Exception)
            {

                return new Response<Product>(false, HttpStatusCode.InternalServerError, ResponseMessages.Error, null);
            }
        }
        #endregion

        #region [- FindByIdAsync(Guid id)-]
        public async Task<IResponse<Product>> FindByIdAsync(Guid id)
        {
            try
            {
                var q = await _context.Product.FindAsync(id);
                return new Response<Product>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, q);
            }
            catch (Exception)
            {

                return new Response<Product>(false, HttpStatusCode.InternalServerError, ResponseMessages.Error, null);
            }
        }
        #endregion

        #region [- InsertAsync(Product obj)-]

        public async Task<IResponse<Product>> InsertAsync(Product obj)
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

                return new Response<Product>(false, HttpStatusCode.InternalServerError, ResponseMessages.Error, null);
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

        public async Task<IResponse<List<Product>>> Select(Product obj)
        {
            try
            {
                var products = await _context.Product.ToListAsync();
                return new Response<List<Product>>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, products);
            }
            catch (Exception)
            {
                return new Response<List<Product>>(false, HttpStatusCode.InternalServerError, ResponseMessages.Error, null);
            }
        }

        #endregion

        #region [- UpdateAsync(Product obj)-]

        public async Task<IResponse<Product>> UpdateAsync(Product obj)
        {
            try
            {
                if (obj is null)
                {
                    return new Response<Product>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }
                _context.Product.Attach(obj);
                _context.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new Response<Product>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation,obj);
            }
            catch (Exception)
            {
                return new Response<Product>(false, HttpStatusCode.InternalServerError, ResponseMessages.Error, null);
            }
        }

        #endregion
    }
}