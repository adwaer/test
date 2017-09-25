using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using backend.Domain.Commands;
using backend.Domain.Entities;
using backend.Domain.QueryConditions;
using backend.WebApi.ViewModels;
using In.Cqrs;
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
        private readonly IMessageSender _messageSender;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="queryBuilder"></param>
        /// <param name="messageSender"></param>
        public BalanceController(IQueryBuilder queryBuilder, IMessageSender messageSender)
        {
            _queryBuilder = queryBuilder;
            _messageSender = messageSender;
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
                UserId = sourceUser.Id
            });

            await _messageSender.SendAsync(new MovementCommand
            {
                Amount = model.Amount,
                Description = $"from {sourceUser.UserName}",
                MakeUserId = sourceUser.Id,
                UserId = targetUser.Id
            });

            return Ok();
        }
    }
}