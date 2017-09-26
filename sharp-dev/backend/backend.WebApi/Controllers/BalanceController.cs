using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using backend.Domain.Entities;
using In.Cqrs.Query;
using In.Cqrs.Query.Criterion;

namespace backend.WebApi.Controllers
{
    /// <summary>
    /// balance api
    /// </summary>
    [Authorize]
    public class BalanceController : ApiController
    {
        private readonly IQueryBuilder _queryBuilder;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="queryBuilder"></param>
        public BalanceController(IQueryBuilder queryBuilder)
        {
            _queryBuilder = queryBuilder;
        }

        /// <summary>
        /// Get balance
        /// </summary>
        /// <returns></returns>
        public async Task<IHttpActionResult> Get()
        {
            var workingDay = await _queryBuilder.For<WorkingDay>()
                .WithAsync(new GenericCriterion<string>(HttpContext.Current.User.Identity.Name));

            return Ok(workingDay?.Balance ?? 0);
        }
        
    }
}