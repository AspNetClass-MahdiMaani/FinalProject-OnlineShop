using OnlineShop.ApplicationServices.Dtos.ProductDtos;

namespace OnlineShop.ApplicationServices.Contracts
{
    public interface IProductService:
        IService<PostProductServiceDto,GetProductServiceDto,GetAllProductServiceDto,PutProductServiceDto,DeleteProductServiceDto>
    {
    }
}
