using OnlineShop.ApplicationServices.Contracts;
using OnlineShop.ApplicationServices.Dtos.ProductDtos;
using OnlineShop.Frameworks;
using OnlineShop.Frameworks.ResponseFrameworks;
using OnlineShop.Frameworks.ResponseFrameworks.Contracts;
using OnlineShop.Models.DomainModels.ProductAggregates;
using OnlineShop.Models.Services.Contracts;
using System.Net;

namespace OnlineShop.ApplicationServices.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        #region [- Ctor -]
        public ProductService(IProductRepository productRepository)
        {
                _productRepository = productRepository;
        }
        #endregion

        #region [- Delete() -]

        public async Task<IResponse<DeleteProductServiceDto>> Delete(DeleteProductServiceDto dto)
        {
            if (dto is null || dto.Id == Guid.Empty)
            {
                return new Response<DeleteProductServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }

            var product = new Product()
            {
                Id = dto.Id,
            };

            var deleteResponse = await _productRepository.DeleteAsync(product);
            if (deleteResponse is null || !deleteResponse.IsSuccessful)
            {
                return new Response<DeleteProductServiceDto>(false, deleteResponse?.Status ?? HttpStatusCode.UnprocessableContent, deleteResponse?.Message ?? ResponseMessages.NullInput, null);
            }

            var response = new Response<DeleteProductServiceDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, dto);
            return response;
        }

        #endregion

        #region [- Get() -]
        public async Task<IResponse<GetProductServiceDto>> Get(GetProductServiceDto dto)
        {
            var Product = new Product()
            {
                Id = dto.Id,
                Title = dto.Title,
                UnitPrice = dto.UnitPrice,
                Description=dto.Description,
            };
            var selectResponse = await _productRepository.Select(Product);

            if (selectResponse is null)
            {
                return new Response<GetProductServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }

            if (!selectResponse.IsSuccessful)
            {
                return new Response<GetProductServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.Error, null);
            }
            var getProductServiceDto = new GetProductServiceDto()
            {
                Id = selectResponse.Value.Id,
                Title = selectResponse.Value.Title,
                UnitPrice = selectResponse.Value.UnitPrice,
                Description = selectResponse.Value.Description,
            };
            var response = new Response<GetProductServiceDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, getProductServiceDto);
            return response;
        }
        #endregion

        #region [- GetAll() -]

        public async Task<IResponse<GetAllProductServiceDto>> GetAll()
        {
            var selectAllResponse = await _productRepository.SelectAll();

            if (selectAllResponse is null)
            {
                return new Response<GetAllProductServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }

            if (!selectAllResponse.IsSuccessful)
            {
                return new Response<GetAllProductServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.Error, null);
            }

            var getAllProductDto = new GetAllProductServiceDto() { GetProductServiceDtos = new List<GetProductServiceDto>() };

            foreach (var item in selectAllResponse.Value)
            {
                var productDto = new GetProductServiceDto()
                {
                    Id = item.Id,
                    Title=item.Title,
                    UnitPrice=item.UnitPrice,
                    Description=item.Description,
                };
                getAllProductDto.GetProductServiceDtos.Add(productDto);
            }

            var response = new Response<GetAllProductServiceDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, getAllProductDto);
            return response;
        }

        #endregion

        #region [- Post() -]

        public async Task<IResponse<PostProductServiceDto>> Post(PostProductServiceDto dto)
        {
            if (dto is null)
            {
                return new Response<PostProductServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }
            var postedProduct = new Product()
            {
                Id = new Guid(),
                Title = dto.Title,
                UnitPrice = dto.UnitPrice,
                Description=dto.Description,
            };
            var insertedResponse = await _productRepository.InsertAsync(postedProduct);

            if (!insertedResponse.IsSuccessful)
            {
                return new Response<PostProductServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.Error, dto);
            }

            var response = new Response<PostProductServiceDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, dto);
            return response;
        }

        #endregion

        #region [- Put() -]

        public async Task<IResponse<PutProductServiceDto>> Put(PutProductServiceDto dto)
        {
            if (dto is null)
            {
                return new Response<PutProductServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }
            var putProduct = new Product()
            {
                Id = dto.Id,
                Title = dto.Title,
                UnitPrice = dto.UnitPrice,
                Description = dto.Description,
            };
            var updateResponse = await _productRepository.UpdateAsync(putProduct);
            if (!updateResponse.IsSuccessful)
            {
                return new Response<PutProductServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }
            var response = new Response<PutProductServiceDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, dto);
            return response;
        }

        #endregion
    }
}
