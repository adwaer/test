using System;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using PM.Configuration;
using PM.Domain.UserContext.Aggregates;
using PM.Domain.UserContext.Commands;
using PM.Models;

namespace PM.CommandHandlers
{
    /// <summary>
    /// user command handlers
    /// </summary>
    public class UserHandlers : ICommandHandler<CreateUserCommand>
    {
        private readonly IIdentityUnitOfWork _uow;

        public UserHandlers(IIdentityUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result> Handle(CreateUserCommand message)
        {
            //todo: move to vaildation decorator
            if (await _uow.UserManager.FindByEmailAsync(message.Email) != null)
                return Result.Fail("Email is already registered");

            // todo: need mapping
            var customer = new Customer
            {
                UserName = message.Email,
                Email = message.Email
            };

            return await new UserAggregate(customer)
                .GenerateSalt()
                .OnSuccess(async aggregate =>
                {
                    var result = await _uow.UserManager.CreateAsync(aggregate.Customer, message.Password);
                    if (!result.Succeeded)
                        return Result.Fail(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));

                    await _uow.SaveChangesAsync();
                    return Result.Ok(aggregate);
                });
        }
    }
}