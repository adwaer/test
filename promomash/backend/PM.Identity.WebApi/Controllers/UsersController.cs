using System.Net;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PM.Domain.Exceptions;
using PM.Domain.UserContext.Commands;
using PM.Domain.UserContext.Queries;
using PM.Identity.WebApi.Configuration;
using PM.Identity.WebApi.Models;
using SilentNotary.Common;
using SilentNotary.Cqrs.Queries;

namespace PM.Identity.WebApi.Controllers
{
    /// <summary>
    /// Controller API for work with users.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UsersController : ControllerBase
    {
        private readonly IQueryBuilder _queryBuilder;
        private readonly IMessageSender _messageSender;

        /// <summary>
        /// Initializes a new instance of the class. 
        /// </summary>
        public UsersController(IQueryBuilder queryBuilder, IMessageSender messageSender)
        {
            _queryBuilder = queryBuilder;
            _messageSender = messageSender;
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="createAccountRequest">Query model 'Create account'.</param>
        /// <returns>Status code.</returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(UserTokenQueryResult))]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        [ProducesResponseType(401)]
        [ProducesResponseType(404, Type = typeof(ErrorResponse))]
        public async Task<ActionResult> CreateUser([FromBody] CreateAccountRequest createAccountRequest)
        {
            var result = await CreateUserCommand
                .Create(createAccountRequest.Email, createAccountRequest.Password, createAccountRequest.ConfirmPassword)
                .OnSuccess(async user => await _messageSender.SendAsync(user), true)
                .OnSuccess(async () =>
                    {
                        var token = await _queryBuilder.For<UserTokenQueryResult>()
                            .WithAsync(new UserTokenQueryCriterion(createAccountRequest.Email,
                                createAccountRequest.Password));

                        return Result.Ok(token);
                    }
                );

            if (!result.IsSuccess)
            {
                throw new InvalidRequestException(result.Error);
            }

            return Ok(result.Value);
        }

    }
}