using Microsoft.AspNetCore.Mvc;
using OnlineShop.ApplicationServices.Dtos.ProductDtos;
using OnlineShop.Models.DomainModels.ProductAggregates;
using OnlineShop.Models.Services.Contracts;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductController> _logger;

        #region [- Ctor -]
        public ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }
        #endregion

        #region [- Get() -]

        [HttpGet(Name = "GetProducts")]
        public async Task<IActionResult> Get(GetProductDto getProductDto)
        {
            if (getProductDto != null)
            {
                var getProduct = new Product()
                {
                    Id=getProductDto.Id,
                    Title=getProductDto.Title,
                    UnitPrice=getProductDto.UnitPrice,
                    Description=getProductDto.Description,
                };
                await _productRepository.Select(getProduct);
            }
            return Ok();
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
