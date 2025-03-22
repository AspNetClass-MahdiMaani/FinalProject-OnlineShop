using Microsoft.EntityFrameworkCore;
using OnlineShop.Frameworks;
using OnlineShop.Frameworks.ResponseFrameworks;
using OnlineShop.Frameworks.ResponseFrameworks.Contracts;
using OnlineShop.Models.DomainModels.personAggregates;
using OnlineShop.Models.DomainModels.ProductAggregates;
using OnlineShop.Models.Services.Contracts;
using System.Net;

namespace OnlineShop.Models.Services.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly FinalProjectDbContext _context;

        #region [-Ctor-]
        public PersonRepository(FinalProjectDbContext context)
        {
            _context = context;
        }
        #endregion

        #region [- DeleteAsync(Person obj) -]

        public async Task<IResponse<Person>> DeleteAsync(Person obj)
        {
            try
            {
                var existingPerson = await _context.Person.FindAsync(obj.Id);
                if (existingPerson == null)
                {
                    return new Response<Person>(false, HttpStatusCode.NotFound, ResponseMessages.NullInput, null);
                }

                _context.Entry(existingPerson).State = EntityState.Detached;
                _context.Person.Remove(existingPerson);
                await SaveChanges();
                return new Response<Person>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, obj);
            }
            catch (Exception)
            {

                return new Response<Person>(false, HttpStatusCode.InternalServerError, ResponseMessages.Error, null);
            }
        }

        #endregion

        #region [- InsertAsync(Person obj) -]

        public async Task<IResponse<Person>> InsertAsync(Person obj)
        {
            try
            {
                if (obj == null)
                {
                    return new Response<Person>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }
                _context.Person.AddAsync(obj);
                await SaveChanges();
                return new Response<Person>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, obj);
            }
            catch (Exception)
            {

                return new Response<Person>(false, HttpStatusCode.InternalServerError, ResponseMessages.Error, null);
            }
        }

        #endregion

        #region [- SaveChanges() -]

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        #endregion

        #region [- Select() -]
        public async Task<IResponse<Person>> Select(Person person)
        {
            try
            {
                var responseValue = new Person();
                responseValue = await _context.Person.FindAsync(person.Id);
                return responseValue is null ?
                     new Response<Person>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null) :
                     new Response<Person>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, responseValue);
            }
            catch (Exception)
            {
                return new Response<Person>(false, HttpStatusCode.InternalServerError, ResponseMessages.Error, null);
            }
        }
        #endregion

        #region [- SelectAll() -]

        public async Task<IResponse<IEnumerable<Person>>> SelectAll()
        {
            try
            {
                var persons = await _context.Person.AsNoTracking().ToListAsync();
                return persons is null ?
                    new Response<IEnumerable<Person>>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null) :
                    new Response<IEnumerable<Person>>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, persons);
            }
            catch (Exception)
            {
                return new Response<IEnumerable<Person>>(false, HttpStatusCode.InternalServerError, ResponseMessages.Error, null);
            }
        }

        #endregion

        #region [- UpdateAsync(Person obj) -]

        public async Task<IResponse<Person>> UpdateAsync(Person obj)
        {
            try
            {
                if (obj is null)
                {
                    return new Response<Person>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }
                _context.Person.Attach(obj);
                _context.Entry(obj).State = EntityState.Modified;
                await SaveChanges();
                return new Response<Person>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, obj);
            }
            catch (Exception)
            {
                return new Response<Person>(false, HttpStatusCode.InternalServerError, ResponseMessages.Error, null);
            }
        }

        #endregion

    }
}
