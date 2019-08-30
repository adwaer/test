namespace PM.Identity.WebApi.Models
{
    /// <summary>
    /// Query model 'Create account'.
    /// </summary>
    public sealed class CreateAccountRequest
    {
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Password Confirmation
        /// </summary>
        public string ConfirmPassword { get; set; }
    }
}