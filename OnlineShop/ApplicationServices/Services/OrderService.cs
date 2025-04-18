using OnlineShop.ApplicationServices.Contracts;
using OnlineShop.ApplicationServices.Dtos.OrderDtos;
using OnlineShop.Frameworks;
using OnlineShop.Frameworks.ResponseFrameworks;
using OnlineShop.Frameworks.ResponseFrameworks.Contracts;
using OnlineShop.Models.DomainModels.OrderAggregates;
using OnlineShop.Models.Services.Contracts;
using System.Net;

namespace OnlineShop.ApplicationServices.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        #region [- Ctor -]

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        #endregion

        #region [- Delete() -]

        public async Task<IResponse<DeleteOrderDto>> Delete(DeleteOrderDto dto)
        {
            try
            {
                var existingOrder = await _orderRepository.Select(new OrderHeader { Id = dto.OrderHeaderId });
                if (!existingOrder.IsSuccessful || existingOrder.Value == null)
                    return new Response<DeleteOrderDto>(false, HttpStatusCode.NotFound, ResponseMessages.Error, null);

                var result = await _orderRepository.DeleteAsync(existingOrder.Value);
                if (!result.IsSuccessful)
                    return new Response<DeleteOrderDto>(false, HttpStatusCode.BadRequest, ResponseMessages.Error, null);

                var responseDto = new DeleteOrderDto
                {
                    OrderHeaderId = dto.OrderHeaderId
                };

                return new Response<DeleteOrderDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, responseDto);
            }
            catch (Exception)
            {
                return new Response<DeleteOrderDto>(false, HttpStatusCode.InternalServerError, ResponseMessages.Error, null);
            }
        }

        #endregion

        #region [- Get() -]

        public async Task<IResponse<GetOrderDto>> Get(GetOrderDto dto)
        {
            try
            {
                if (dto.OrderHeaderId == null)
                    return new Response<GetOrderDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);

                var order = await _orderRepository.Select(new OrderHeader { Id = dto.OrderHeaderId.Value });

                if (!order.IsSuccessful || order.Value == null)
                    return new Response<GetOrderDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);

                var result = new GetOrderDto
                {
                    OrderHeaderId = order.Value.Id,
                    UnitPrice = order.Value.OrderDetails?.Sum(od => od.UnitPrice) ?? 0,
                    Amount = order.Value.OrderDetails?.Sum(od => od.Amount) ?? 0,
                    SellerId = order.Value.SellerId,
                    BuyerId = order.Value.BuyerId
                };

                return new Response<GetOrderDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, result);
            }
            catch (Exception)
            {
                return new Response<GetOrderDto>(false, HttpStatusCode.InternalServerError, ResponseMessages.Error, null);
            }
        }

        #endregion

        #region [- GetAll() -]

        public async Task<IResponse<GetAllOrderDto>> GetAll()
        {
            try
            {
                var orders = await _orderRepository.SelectAll();
                if (!orders.IsSuccessful || orders.Value == null)
                    return new Response<GetAllOrderDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);

                var orderDtos = orders.Value.Select(order => new GetOrderDto
                {
                    OrderHeaderId = order.Id,
                    UnitPrice = order.OrderDetails?.Sum(od => od.UnitPrice) ?? 0,
                    Amount = order.OrderDetails?.Sum(od => od.Amount) ?? 0,
                    SellerId = order.SellerId,
                    BuyerId = order.BuyerId
                }).ToList();

                var result = new GetAllOrderDto
                {
                    GetOrderDtos = orderDtos
                };

                return new Response<GetAllOrderDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, result);
            }
            catch (Exception)
            {
                return new Response<GetAllOrderDto>(false, HttpStatusCode.InternalServerError, ResponseMessages.Error, null);
            }
        }
        #endregion

        #region [- Post() -]

        public async Task<IResponse<PostOrderDto>> Post(PostOrderDto dto)
        {
            try
            {
                if (dto.SellerId == dto.BuyerId)
                {
                    return new Response<PostOrderDto>(false, HttpStatusCode.BadRequest,
                        "Seller and Buyer cannot be the same", null);
                }

                var orderHeader = new OrderHeader
                {
                    Id = Guid.NewGuid(),
                    SellerId = dto.SellerId,
                    BuyerId = dto.BuyerId,
                    OrderDetails = new List<OrderDetail>
                    {
                       new OrderDetail
                       {
                          UnitPrice = dto.UnitPrice,
                          Amount = dto.Amount,
                          ProductId = dto.ProductId
                       }
                    }
                };

                var result = await _orderRepository.InsertAsync(orderHeader);
                if (!result.IsSuccessful)
                {
                    return new Response<PostOrderDto>(false, HttpStatusCode.BadRequest,
                        result.Message, null);
                }

                return new Response<PostOrderDto>(true, HttpStatusCode.OK,
                    ResponseMessages.SuccessfullOperation, dto);
            }
            catch (Exception)
            {
                return new Response<PostOrderDto>(false, HttpStatusCode.InternalServerError,
                    ResponseMessages.Error, null);
            }
        }
        #endregion

        #region [- Put() -]
        public async Task<IResponse<PutOrderDto>> Put(PutOrderDto dto)
        {
            try
            {
                if (dto.OrderHeaderId == null)
                {
                    return new Response<PutOrderDto>(false, HttpStatusCode.BadRequest, ResponseMessages.NullInput, null);
                }

                var existingOrder = await _orderRepository.Select(new OrderHeader { Id = dto.OrderHeaderId });

                if (!existingOrder.IsSuccessful || existingOrder.Value == null)
                    return new Response<PutOrderDto>(false, HttpStatusCode.NotFound, ResponseMessages.Error, null);

                existingOrder.Value.OrderDetails = new List<OrderDetail>
            {
                new OrderDetail
                {
                    UnitPrice = dto.UnitPrice,
                    Amount = dto.Amount,
                    ProductId = dto.ProductId
                }
            };

                var result = await _orderRepository.UpdateAsync(existingOrder.Value);
                if (!result.IsSuccessful)
                    return new Response<PutOrderDto>(false, HttpStatusCode.BadRequest, ResponseMessages.Error, null);

                var responseDto = new PutOrderDto
                {
                    OrderHeaderId = dto.OrderHeaderId,
                    ProductId = dto.ProductId,
                    UnitPrice = dto.UnitPrice,
                    Amount = dto.Amount
                };

                return new Response<PutOrderDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, responseDto);
            }
            catch (Exception)
            {
                return new Response<PutOrderDto>(false, HttpStatusCode.InternalServerError, ResponseMessages.Error, null);
            }
        }
        #endregion
    }
}
