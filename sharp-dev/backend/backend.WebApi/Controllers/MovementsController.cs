using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using backend.Domain.Commands;
using backend.Domain.Entities;
using backend.Domain.QueryConditions;
using backend.Domain.QueryResults;
using backend.WebApi.ViewModels;
using In.Cqrs;
using In.Cqrs.Query;

namespace backend.WebApi.Controllers
{
    /// <summary>
    /// movements api
    /// </summary>
    [Authorize]
    public class MovementsController : ApiController
    {
        private readonly IMessageSender _messageSender;
        private readonly IQueryBuilder _queryBuilder;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="messageSender"></param>
        /// <param name="queryBuilder"></param>
        public MovementsController(IMessageSender messageSender, IQueryBuilder queryBuilder)
        {
            _messageSender = messageSender;
            _queryBuilder = queryBuilder;
        }

        /// <summary>
        /// Get movements list
        /// </summary>
        /// <returns></returns>
        public async Task<IHttpActionResult> Get([FromUri]MovementsQueryCondition model)
        {
            model = model ?? new MovementsQueryCondition();
            model.UserId = HttpContext.Current.User.Identity.Name;

            var movements = await _queryBuilder.For<IMultipleQueryResult<MovementsQueryResult>>()
                .WithAsync(model);

            return Ok(movements);
        }

        /// <summary>
        /// Change balance
        /// </summary>
        /// <returns></returns>
        public async Task<IHttpActionResult> Post(MovementViewModel model)
        {
            if (model.Amount <= 0)
            {
                return BadRequest("the amount should be positive");
            }

            var sourceUser = await _queryBuilder.For<ApplicationUser>()
                .WithAsync(new UserByIdQueryCondition
                {
                    Id = HttpContext.Current.User.Identity.Name
                });

            var targetUser = await _queryBuilder.For<ApplicationUser>()
                .WithAsync(new UserByEmailQueryCondition
                {
                    Email = model.TargetEmail
                });

            await _messageSender.SendAsync(new MovementCommand
            {
                Amount = -model.Amount,
                Description = $"sent money to {targetUser.UserName}",
                UserId = sourceUser.Id,
                CorrespondentId = targetUser.Id
            });

            await _messageSender.SendAsync(new MovementCommand
            {
                Amount = model.Amount,
                Description = $"from {sourceUser.UserName}",
                UserId = targetUser.Id,
                CorrespondentId = sourceUser.Id
            });

            return Ok();
        }
    }
}
