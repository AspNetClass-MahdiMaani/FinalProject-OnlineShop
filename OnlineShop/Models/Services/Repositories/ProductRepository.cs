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
        private readonly FinalProjectDbContext _context;

        #region [-Ctor-]

        public ProductRepository(FinalProjectDbContext context)
        {
            _context = context;
        }
        #endregion

        #region [- DeleteAsync(Product obj)-]

        public async Task<IResponse<Product>> DeleteAsync(Product obj)
        {
            try
            {
                var existingProduct = await _context.Product.FindAsync(obj.Id);
                if (existingProduct == null)
                {
                    return new Response<Product>(false, HttpStatusCode.NotFound, ResponseMessages.NullInput, null);
                }

                _context.Entry(existingProduct).State = EntityState.Detached;
                _context.Product.Remove(existingProduct);
                await SaveChanges();

                return new Response<Product>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, existingProduct);
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

        #region [- Select() -]
        public async Task<IResponse<Product>> Select(Product Product)
        {
            try
            {
                var responseValue = new Product();
                responseValue = await _context.Product.FindAsync(Product.Id);
                return responseValue is null ?
                     new Response<Product>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null) :
                     new Response<Product>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, responseValue);
            }
            catch (Exception)
            {
                return new Response<Product>(false, HttpStatusCode.InternalServerError, ResponseMessages.Error, null);
            }
        }
        #endregion

        #region [- SelectAll() -]

        public async Task<IResponse<IEnumerable<Product>>> SelectAll()
        {
            try
            {
                var products = await _context.Product.AsNoTracking().ToListAsync();
                return products is null ?
                    new Response<IEnumerable<Product>>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null) :
                    new Response<IEnumerable<Product>>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, products);
            }
            catch (Exception)
            {
                return new Response<IEnumerable<Product>>(false, HttpStatusCode.InternalServerError, ResponseMessages.Error, null);
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
                await SaveChanges();
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