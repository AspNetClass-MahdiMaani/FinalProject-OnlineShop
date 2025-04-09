using Microsoft.AspNetCore.Mvc;
using OnlineShop.ApplicationServices.Contracts;
using OnlineShop.ApplicationServices.Dtos.OrderDtos;

namespace OnlineShop.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        #region [- Ctor() -]
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        #endregion

        #region [- GetAll() -]

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _orderService.GetAll();

            if (!result.IsSuccessful)
                return StatusCode((int)result.Status, result.Message);

            return new JsonResult(result.Value);
        }
        #endregion

        #region [- Get() -]

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _orderService.Get(new GetOrderDto { OrderHeaderId = id });

            if (!result.IsSuccessful)
                return StatusCode((int)result.Status, result.Message);

            return new JsonResult(result.Value);
        }

        #endregion

        #region [- Post() -]

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostOrderDto dto)
        {
            var result = await _orderService.Post(dto);

            if (!result.IsSuccessful)
                return StatusCode((int)result.Status, result.Message);

            return new JsonResult(result);
        }

        #endregion

        #region [- Update() -]

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] PutOrderDto dto)
        {
            if (id != dto.OrderHeaderId)
                return BadRequest("OrderHeaderId in the URL does not match the one in the request body.");

            var result = await _orderService.Put(dto);

            if (!result.IsSuccessful)
                return StatusCode((int)result.Status, result.Message);

            return new JsonResult(result.Value);
        }

        #endregion

        #region [- Delete() -]

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var result = await _orderService.Delete(new DeleteOrderDto { OrderHeaderId = id });

            if (!result.IsSuccessful)
                return StatusCode((int)result.Status, result.Message);

            return NoContent();
        }
        #endregion

    }
}
