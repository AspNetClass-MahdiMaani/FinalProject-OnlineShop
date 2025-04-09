using Microsoft.AspNetCore.Mvc;
using OnlineShop.ApplicationServices.Contracts;
using OnlineShop.ApplicationServices.Dtos.PersonDtos;

namespace OnlineShop.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        #region [- ctor -]
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }
        #endregion

        #region [- GetAll() -]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            Guard_PersonService();
            var getAllResponse = await _personService.GetAll();
            var response = getAllResponse.Value.GetPersonServiceDtos;
            return new JsonResult(response);
        }
        #endregion

        #region [- Get() -]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            Guard_PersonService();
            var dto = new GetPersonServiceDto() { Id = id };
            var getResponse = await _personService.Get(dto);
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
        public async Task<IActionResult> Post([FromBody] PostPersonServiceDto dto)
        {
            Guard_PersonService();
            var postedDto = new GetPersonServiceDto() { FName = dto.FName, LName=dto.LName };
            var getResponse = await _personService.Get(postedDto);

            if (ModelState.IsValid && getResponse.Value is null)
            {
                var postResponse = await _personService.Post(dto);
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
        public async Task<IActionResult> Put(PutPersonServiceDto dto)
        {
            Guard_PersonService();
            var updatePerson = await _personService.Put(dto);
            return new JsonResult(updatePerson);
        }
        #endregion

        #region [- Delete() -]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeletePersonServiceDto dto)
        {
            Guard_PersonService();

            var getDto = new GetPersonServiceDto()
            {
                Id = dto.Id
            };
            var person = await _personService.Get(getDto);
            if (person == null)
            {
                return NotFound("Person not found");
            }
            var isDeleted = await _personService.Delete(dto);
            return NoContent();
        }
        #endregion

        #region [- PersonServiceGuard() -]
        private ObjectResult Guard_PersonService()
        {
            if (_personService is null)
            {
                return Problem($" {nameof(_personService)} is null.");
            }

            return null;
        }
        #endregion

    }
}
