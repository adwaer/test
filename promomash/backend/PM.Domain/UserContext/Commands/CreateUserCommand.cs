using CSharpFunctionalExtensions;
using PM.Domain.Validators;
using SilentNotary.Common;
using SilentNotary.FunctionalCSharp;

namespace PM.Domain.UserContext.Commands
{
    public class CreateUserCommand : IMessage
    {
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        public CreateUserCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public static Result<CreateUserCommand> Create(string email, string password, string passwordConfirmation)
        {
            return ParametersValidation.Validate(
                    ParametersValidation.NotNull(email, "Mandatory field 'Email' is not filled"),
                    ParametersValidation.Ensure(() => new EmailAttribute().IsValid(email), nameof(email)),
                    ParametersValidation.NotNull(passwordConfirmation, "Mandatory field 'Password Confirmation' is not filled"),
                    ParametersValidation.NotNull(password, "Mandatory field 'Password' is not filled"),
                    ParametersValidation.Ensure(() => password.Equals(passwordConfirmation), "Passwords do not match")
                )
                .OnAllSuccess(() => new CreateUserCommand(email, password));
        }
    }
}