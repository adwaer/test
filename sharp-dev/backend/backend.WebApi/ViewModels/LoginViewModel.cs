using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace backend.WebApi.ViewModels
{
    /// <summary>
    /// login model
    /// </summary>
    [DataContract]
    public class LoginViewModel
    {
        /// <summary>
        /// login
        /// </summary>
        [DataMember, Required, EmailAddress]
        public string Login { get; set; }
        /// <summary>
        /// password
        /// </summary>
        [DataMember, Required]
        public string Password { get; set; }
    }
}