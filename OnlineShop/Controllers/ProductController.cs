using Microsoft.AspNetCore.Mvc;
using OnlineShop.ApplicationServices.Contracts;
using OnlineShop.ApplicationServices.Dtos.PersonDtos;
using OnlineShop.ApplicationServices.Dtos.ProductDtos;
using OnlineShop.ApplicationServices.Services;

namespace OnlineShop.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        #region [- Ctor -]
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        #endregion

        #region [- GetAll() -]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            Guard_ProductService();
            var getAllResponse = await _productService.GetAll();
            var response = getAllResponse.Value.GetProductServiceDtos;
            return new JsonResult(response);
        }
        #endregion

        #region [- Get() -]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            Guard_ProductService();
            var dto = new GetProductServiceDto() { Id = id };
            var getResponse = await _productService.Get(dto);
            var response = getResponse.Value;
            if (response is null)
            {
                return new JsonResult("NotFound");
            }
            return new JsonResult(response);
        }
        #endregion

        #region [- Post() -]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostProductServiceDto dto)
        {
            Guard_ProductService();
            var postedDto = new GetProductServiceDto() { Title = dto.Title, UnitPrice = dto.UnitPrice, Description=dto.Description };
            var getResponse = await _productService.Get(postedDto);

            if (ModelState.IsValid && getResponse.Value is null)
            {
                var postResponse = await _productService.Post(dto);
                return new JsonResult(postResponse);
            }
            else if (ModelState.IsValid && getResponse.Value is not null)
            {
                return Conflict(dto);
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion

        #region [- Put() -]

        [HttpPut]
        public async Task<IActionResult> Put(PutProductServiceDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Input is null");
            }
            Guard_ProductService();
            var updateProduct = await _productService.Put(dto);
            return new JsonResult(updateProduct);
        }
        #endregion

        #region [- Delete() -]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteProductServiceDto dto)
        {
            Guard_ProductService();

            var getDto = new GetProductServiceDto()
            {
                Id = dto.Id
            };
            var product = await _productService.Get(getDto); 
            if (product == null)
            {
                return NotFound("Product not found");
            }
            var isDeleted = await _productService.Delete(dto); 
            return NoContent(); 
        }
        #endregion

        #region [- ProductServiceGuard() -]
        private ObjectResult Guard_ProductService()
        {
            if (_productService is null)
            {
                return Problem($" {nameof(_productService)} is null.");
            }

            return null;
        }
        #endregion
    }
}
