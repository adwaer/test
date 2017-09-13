using System;
using System.Threading.Tasks;
using System.Web.Http;
using backend.Domain;
using In.Cqrs.Query;
using In.Cqrs.Query.Criterion;

namespace backend.WebApi.Controllers
{
    /// <summary>
    /// address info api
    /// </summary>
    [Authorize]
    public class AbInfoController : ApiController
    {
        private readonly IQueryBuilder _queryBuilder;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="queryBuilder"></param>
        public AbInfoController(IQueryBuilder queryBuilder)
        {
            _queryBuilder = queryBuilder;
        }

        /// <summary>
        /// gets address books
        /// </summary>
        /// <returns></returns>
        public async Task<IHttpActionResult> Get()
        {
            var result = await _queryBuilder
                .For<AddressBook[]>()
                .WithAsync(new EmptyCriterion());

            return Ok(result);
        }

        /// <summary>
        /// gets address books by id
        /// </summary>
        /// <returns></returns>
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var result = await _queryBuilder
                    .For<AddressBook>()
                    .WithAsync(new GenericCriterion<int>(id));

                return Ok(result);
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Запись не найдена");
            }
        }
    }
}