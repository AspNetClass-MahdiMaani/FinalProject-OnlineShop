﻿using Microsoft.EntityFrameworkCore;
using OnlineShop.Frameworks;
using OnlineShop.Frameworks.ResponseFrameworks;
using OnlineShop.Frameworks.ResponseFrameworks.Contracts;
using OnlineShop.Models.DomainModels.OrderAggregates;
using OnlineShop.Models.DomainModels.personAggregates;
using OnlineShop.Models.Services.Contracts;
using System.Net;

namespace OnlineShop.Models.Services.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly FinalProjectDbContext _context;

        #region [- Ctor -]

        public OrderRepository(FinalProjectDbContext context)
        {
            _context = context;
        }

        #endregion

        #region [- SelectAll() -]

        public async Task<IResponse<IEnumerable<OrderHeader>>> SelectAll()
        {
            try
            {
                var orders = await _context.Set<OrderHeader>()
                    .Include(o => o.OrderDetails)
                    .ToListAsync();
                return new Response<IEnumerable<OrderHeader>>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, orders);
            }
            catch (Exception)
            {
                return new Response<IEnumerable<OrderHeader>>(false, HttpStatusCode.InternalServerError, ResponseMessages.Error, null);
            }
        }

        #endregion

        #region [- InsertAsync() -]
        public async Task<IResponse<OrderHeader>> InsertAsync(OrderHeader obj)
        {
            try
            {
                if (obj.SellerId == default || obj.BuyerId == default)
                {
                    return new Response<OrderHeader>(false, HttpStatusCode.BadRequest,
                        "Seller or Buyer ID is missing", null);
                }

                if (obj.SellerId == obj.BuyerId)
                {
                    return new Response<OrderHeader>(false, HttpStatusCode.BadRequest,
                        "Seller and Buyer cannot be the same", null);
                }

                var seller = await _context.Set<Person>().FindAsync(obj.SellerId);
                if (seller == null)
                {
                    return new Response<OrderHeader>(false, HttpStatusCode.NotFound,
                        "Seller not found", null);
                }

                var buyer = await _context.Set<Person>().FindAsync(obj.BuyerId);
                if (buyer == null)
                {
                    return new Response<OrderHeader>(false, HttpStatusCode.NotFound,
                        "Buyer not found", null);
                }

                obj.Seller = seller;
                obj.Buyer = buyer;

                _context.Set<OrderHeader>().Add(obj);
                await SaveChanges();

                return new Response<OrderHeader>(true, HttpStatusCode.OK,
                    ResponseMessages.SuccessfullOperation, obj);
            }
            catch (Exception)
            {
                return new Response<OrderHeader>(false, HttpStatusCode.InternalServerError,
                    ResponseMessages.Error, null);
            }
        }
        #endregion

        #region [- UpdateAsync() -]

        public async Task<IResponse<OrderHeader>> UpdateAsync(OrderHeader obj)
        {
            try
            {
                if (obj.SellerId == default || obj.BuyerId == default)
                {
                    return new Response<OrderHeader>(false, HttpStatusCode.BadRequest,
                        "Seller or Buyer ID is missing", null);
                }
                var existingOrder = await _context.OrderHeader
                    .Include(o => o.OrderDetails)
                    .FirstOrDefaultAsync(o => o.Id == obj.Id);

                if (existingOrder == null)
                    return new Response<OrderHeader>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);

                _context.Entry(existingOrder).CurrentValues.SetValues(obj);

                existingOrder.Seller = obj.Seller;
                existingOrder.Buyer = obj.Buyer;

                var existingDetails = existingOrder.OrderDetails.ToList();
                foreach (var existingDetail in existingDetails)
                {
                    if (!obj.OrderDetails.Any(d =>
                        d.OrderHeaderId == existingDetail.OrderHeaderId &&
                        d.ProductId == existingDetail.ProductId))
                    {
                        _context.Set<OrderDetail>().Remove(existingDetail);
                    }
                }
                foreach (var detail in obj.OrderDetails)
                {
                    var existingDetail = existingOrder.OrderDetails
                        .FirstOrDefault(d =>
                            d.OrderHeaderId == detail.OrderHeaderId &&
                            d.ProductId == detail.ProductId);

                    if (existingDetail != null)
                    {
                        _context.Entry(existingDetail).CurrentValues.SetValues(detail);
                    }
                    else
                    {
                        _context.Set<OrderDetail>().Add(detail);
                    }
                }
                await SaveChanges();
                return new Response<OrderHeader>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, obj);
            }
            catch (Exception)
            {
                return new Response<OrderHeader>(false, HttpStatusCode.InternalServerError, ResponseMessages.Error, null);
            }
        }

        #endregion

        #region [- DeleteAsync(OrderHeader obj) -]

        public async Task<IResponse<OrderHeader>> DeleteAsync(OrderHeader obj)
        {
            try
            {
                _context.Set<OrderHeader>().Remove(obj);
                await _context.SaveChangesAsync();
                return new Response<OrderHeader>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, obj);
            }
            catch (Exception)
            {
                return new Response<OrderHeader>(false, HttpStatusCode.InternalServerError, ResponseMessages.Error, null);
            }
        }

        #endregion

        #region [- DeleteAsync(Guid id) -]

        public async Task<IResponse<OrderHeader>> DeleteAsync(Guid id)
        {
            try
            {
                var order = await _context.Set<OrderHeader>().FindAsync(id);
                if (order == null)
                    return new Response<OrderHeader>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);

                _context.Set<OrderHeader>().Remove(order);
                await SaveChanges();
                return new Response<OrderHeader>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, order);
            }
            catch (Exception)
            {
                return new Response<OrderHeader>(false, HttpStatusCode.InternalServerError, ResponseMessages.Error, null);
            }
        }

        #endregion

        #region [- Select() -]

        public async Task<IResponse<OrderHeader>> Select(OrderHeader obj)
        {
            try
            {
                var order = await _context.Set<OrderHeader>()
                    .Include(o => o.OrderDetails)
                    .FirstOrDefaultAsync(o => o.Id == obj.Id);
                if (order == null)
                    return new Response<OrderHeader>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);

                return new Response<OrderHeader>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, order);
            }
            catch (Exception)
            {
                return new Response<OrderHeader>(false, HttpStatusCode.InternalServerError, ResponseMessages.Error, null);
            }
        }

        #endregion

        #region [- SaveChanges() -]

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        #endregion

    }
}
