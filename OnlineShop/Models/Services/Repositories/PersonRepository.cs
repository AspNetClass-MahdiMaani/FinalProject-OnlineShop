using Microsoft.EntityFrameworkCore;
using OnlineShop.Frameworks;
using OnlineShop.Frameworks.ResponseFrameworks;
using OnlineShop.Frameworks.ResponseFrameworks.Contracts;
using OnlineShop.Models.DomainModels.personAggregates;
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
                if (_context.Entry(obj).State == EntityState.Detached)
                {
                    _context.Person.Attach(obj);
                }
                _context.Remove(obj);
                await SaveChanges();
                return new Response<Person>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, obj);
            }
            catch (Exception)
            {

                return new Response<Person>(false, HttpStatusCode.InternalServerError, ResponseMessages.Error, null);
            }
        }

        #endregion

        #region [- DeleteAsync(Guid id) - ]

        public async Task<IResponse<Person>> DeleteAsync(Guid id)
        {
            try
            {
                var entityToDelete = _context.Person.Find(id);
                _context.Remove(entityToDelete);
                await SaveChanges();
                return new Response<Person>(true, HttpStatusCode.Accepted, ResponseMessages.SuccessfullOperation, null);
            }
            catch (Exception)
            {

                return new Response<Person>(false, HttpStatusCode.InternalServerError, ResponseMessages.Error, null);
            }
        }

        #endregion

        #region [- FindByIdAsync(Guid id) -]

        public async Task<IResponse<Person>> FindByIdAsync(Guid id)
        {
            try
            {
                var q = await _context.Person.FindAsync(id);
                return new Response<Person>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, q);
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

        public async Task<IResponse<List<Person>>> Select()
        {
            try
            {
                try
                {
                    var persons = await _context.Person.ToListAsync();
                    return new Response<List<Person>>(true, HttpStatusCode.OK, ResponseMessages.SuccessfullOperation, persons);
                }
                catch (Exception)
                {
                    return new Response<List<Person>>(false, HttpStatusCode.InternalServerError, ResponseMessages.Error, null);
                }
            }
            catch (Exception)
            {

                throw;
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
                await _context.SaveChangesAsync();
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
