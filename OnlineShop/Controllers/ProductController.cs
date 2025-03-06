using Microsoft.AspNetCore.Mvc;
using OnlineShop.ApplicationServices.Dtos.ProductDtos;
using OnlineShop.Models.DomainModels.ProductAggregates;
using OnlineShop.Models.Services.Contracts;

namespace OnlineShop.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        #region [- Ctor -]
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        #endregion

        #region [- Get() -]

        [HttpGet(Name = "GetProducts")]
        public async Task<IActionResult> Get()
        {
            var products = _productRepository.Select().Result;
            return new JsonResult(products);
        }

        #endregion

        #region [- Post() -]
        [HttpPost(Name = "PostProduct")]
        public async Task<IActionResult> Post(InsertProductDto insertProductDto)
        {
            if (insertProductDto != null)
            {
                var product = new Product()
                {
                    Title = insertProductDto.Title,
                    Description = insertProductDto.Description,
                    UnitPrice = insertProductDto.UnitPrice,
                };
                await _productRepository.InsertAsync(product);
            }
            return Ok();
        }

        #endregion

        #region [- Put() -]
        [HttpPut(Name = "PutProduct")]
        public async Task<IActionResult> Put(UpdateProductDto updateProductDto)
        {
            if (updateProductDto != null)
            {
                var product = new Product()
                {
                    Id = updateProductDto.Id,
                    Title = updateProductDto.Title,
                    Description = updateProductDto.Description,
                    UnitPrice = updateProductDto.UnitPrice,
                };
                await _productRepository.UpdateAsync(product);
            }

            return Ok();
        }
        #endregion

        #region [- Delete() -]

        [HttpDelete(Name = "DeleteProduct")]
        public async Task<IActionResult> Delete(DeleteProductDto deleteProductDto)
        {
            if (deleteProductDto != null)
            {
                await _productRepository.DeleteAsync(deleteProductDto.Id);
            }
            return Ok();
        }

        #endregion
    }
}
