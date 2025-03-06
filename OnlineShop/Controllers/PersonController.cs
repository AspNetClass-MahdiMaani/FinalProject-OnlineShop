using Microsoft.AspNetCore.Mvc;
using OnlineShop.ApplicationServices.Dtos.PersonDtos;
using OnlineShop.Models.DomainModels.personAggregates;
using OnlineShop.Models.Services.Contracts;

namespace OnlineShop.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;

        #region [- Ctor -]

        public PersonController(IPersonRepository personRepository)
        {
                _personRepository= personRepository;
        }

        #endregion

        #region [- Get() -]

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var persons = _personRepository.Select().Result;
            return new JsonResult(persons);
        }
        #endregion

        #region [- Post() -]

        [HttpPost]
        public async Task<IActionResult> Post(InsertPersonDto insertPersonDto)
        {
            if (insertPersonDto != null)
            {
                var person = new Person()
                {
                    FName = insertPersonDto.FName,
                    LName = insertPersonDto.LName,
                };
                await _personRepository.InsertAsync(person);
            }
            return Ok();
        }

        #endregion

        #region [- Put() -]

        [HttpPut]
        public async Task<IActionResult> Put(UpdatePersonDto updatePersonDto)
        {
            if (updatePersonDto != null)
            {
                var person = new Person()
                {
                    Id = updatePersonDto.Id,
                    FName = updatePersonDto.FName,
                    LName = updatePersonDto.LName,
                };
                await _personRepository.UpdateAsync(person);
            }

            return Ok();
        }
        #endregion

        #region [- Delete() -]

        [HttpDelete]
        public async Task<IActionResult> Delete(DeletePersonDto deletePersonDto)
        {
            if (deletePersonDto != null)
            {
                await _personRepository.DeleteAsync(deletePersonDto.Id);
            }
            return Ok();
        }

        #endregion

    }
}
