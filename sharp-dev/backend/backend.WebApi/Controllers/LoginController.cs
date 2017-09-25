using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using backend.Domain.Commands;
using backend.Domain.Entities;
using backend.Domain.QueryConditions;
using backend.Identity;
using backend.WebApi.ViewModels;
using In.Cqrs;
using In.Cqrs.Query;
using Microsoft.AspNet.Identity.Owin;

namespace backend.WebApi.Controllers
{
    /// <summary>
    /// Login api
    /// </summary>
    public class LoginController : ApiController
    {
        private readonly IMessageSender _messageSender;
        private readonly IQueryBuilder _queryBuilder;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="messageSender"></param>
        /// <param name="queryBuilder"></param>
        public LoginController(IMessageSender messageSender, IQueryBuilder queryBuilder)
        {
            _messageSender = messageSender;
            _queryBuilder = queryBuilder;
        }

        /// <summary>
        /// Validate login password pair
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Get([FromUri]LoginViewModel model)
        {
            var userManager = Request.GetOwinContext().GetUserManager<CustomUserManager>();
            var user = await userManager.FindAsync(model.Login, model.Password);
            if (user == null)
            {
                return BadRequest("Login or password is not correct");
            }

            var identity = new GenericIdentity(model.Login);
            var principal = new GenericPrincipal(identity, null);

            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }

            return Ok();
        }

        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Post(RegViewModel model)
        {
            var isFrequently = await _queryBuilder.For<bool>()
                .WithAsync(new ValidatePwdFromFrequentlListCondition(model.Password));
            if (isFrequently)
            {
                return BadRequest("This password is in blacklist, choose another");
            }

            var userManager = Request.GetOwinContext().GetUserManager<CustomUserManager>();
            var identityResult = await userManager.CreateAsync(new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email
            }, model.Password);

            if (identityResult.Succeeded)
            {
                var user = await userManager.FindAsync(model.Email, model.Password);

                await _messageSender.SendAsync(new MovementCommand
                {
                    UserId = user.Id,
                    Amount = 500,
                    Description = "for the introduction"
                });
                return Ok();
            }

            var errors = identityResult.Errors.Aggregate("", (current, error) => current + error + "; ");
            return BadRequest(errors);
        }
    }
}
