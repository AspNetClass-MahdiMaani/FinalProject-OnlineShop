using OnlineShop.ApplicationServices.Dtos.OrderDtos;

namespace OnlineShop.ApplicationServices.Contracts
{
    public interface IOrderService : IService<PostOrderDto, GetOrderDto,GetAllOrderDto, PutOrderDto, DeleteOrderDto>
    {

    }
}
