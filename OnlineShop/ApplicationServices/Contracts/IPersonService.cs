using OnlineShop.ApplicationServices.Dtos.PersonDtos;

namespace OnlineShop.ApplicationServices.Contracts
{
    public interface IPersonService :
        IService<PostPersonServiceDto, GetPersonServiceDto, GetAllPersonServiceDto, PutPersonServiceDto, DeletePersonServiceDto>
    {
    }
}
