using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PM.Configuration;
using PM.Domain.Exceptions;
using PM.Domain.UserContext.Queries;
using PM.Domain.Validators;
using SilentNotary.Cqrs.Queries;

namespace PM.QueryHandlers
{
    public class UserTokenQueryHandler : IQuery<UserTokenQueryCriterion, UserTokenQueryResult>
    {
        private readonly AuthenticationConfig _authenticationOptions;
        private readonly IIdentityUnitOfWork _uow;

        public UserTokenQueryHandler(IOptions<AuthenticationConfig> authenticationOptions, IIdentityUnitOfWork uow)
        {
            _authenticationOptions = authenticationOptions.Value;
            _uow = uow;
        }

        public async Task<UserTokenQueryResult> Ask(UserTokenQueryCriterion criterion)
        {
            if (!new EmailAttribute().IsValid(criterion.UserName))
                throw new InvalidRequestException("Incorrect mail format");

            var user = await _uow.UserManager.FindByEmailAsync(criterion.UserName);

            if (user == null)
                throw new UserNotFoundException("User with such mail not found");

            var result = await _uow.SignInManager.PasswordSignInAsync(user.UserName, criterion.Password,
                isPersistent: true,
                lockoutOnFailure: false);

            if (result.IsLockedOut)
                throw new InvalidOperationException("User is blocked");

            if (!result.Succeeded)
                throw new InvalidRequestException("Incorrect password");

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authenticationOptions.SecretJwtKey));
            var jwt = new JwtSecurityToken(
                issuer: _authenticationOptions.Url,
                audience: _authenticationOptions.Url,
                notBefore: DateTime.UtcNow,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromHours(_authenticationOptions.LifeTime)),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256));

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return new UserTokenQueryResult(token);
        }
    }
}