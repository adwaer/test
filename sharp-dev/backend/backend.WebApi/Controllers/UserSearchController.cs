using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using backend.Domain.Entities;
using backend.Domain.QueryConditions;
using backend.Domain.QueryResults;
using In.Cqrs.Query;

namespace backend.WebApi.Controllers
{
    /// <summary>
    /// user search api
    /// </summary>
    [Authorize]
    public class UserSearchController : ApiController
    {
        private readonly IQueryBuilder _queryBuilder;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="queryBuilder"></param>
        public UserSearchController(IQueryBuilder queryBuilder)
        {
            _queryBuilder = queryBuilder;
        }

        /// <summary>
        /// Get users
        /// </summary>
        /// <returns></returns>
        public async Task<IHttpActionResult> Get(string condition)
        {
            var user = await _queryBuilder.For<ApplicationUser>()
                .WithAsync(new UserByIdQueryCondition
                {
                    Id = HttpContext.Current.User.Identity.Name
                });

            var users = await _queryBuilder.For<UsersSearchQueryResult>()
                .WithAsync(new UsersSearchQueryCondition
                {
                    Pattern = condition,
                    ExcludeEmail = user.Email
                });

            return Ok(users);
        }
    }
}