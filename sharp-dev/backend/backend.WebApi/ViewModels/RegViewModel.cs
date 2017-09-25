using System.ComponentModel.DataAnnotations;

namespace backend.WebApi.ViewModels
{
    /// <summary>
    /// reg model
    /// </summary>
    public class RegViewModel
    {
        /// <summary>
        /// user name
        /// </summary>
        [Required]
        public string UserName { get; set; }
        /// <summary>
        /// email
        /// </summary>
        [Required, EmailAddress]
        public string Email { get; set; }
        /// <summary>
        /// password
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}