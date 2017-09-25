using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using backend.Domain.QueryResults;
using In.Cqrs.Query;
using In.Cqrs.Query.Criterion;

namespace backend.WebApi.Controllers
{
    /// <summary>
    /// users data api
    /// </summary>
    [Authorize]
    public class UsersController : ApiController
    {
        private readonly IQueryBuilder _queryBuilder;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="queryBuilder"></param>
        public UsersController(IQueryBuilder queryBuilder)
        {
            _queryBuilder = queryBuilder;
        }

        /// <summary>
        /// Get user data
        /// </summary>
        /// <returns></returns>
        public async Task<IHttpActionResult> Get()
        {
            var userData = await _queryBuilder.For<UserDataQueryResult>()
                .WithAsync(new GenericCriterion<string>(HttpContext.Current.User.Identity.Name));

            return Ok(userData);
        }
    }
}