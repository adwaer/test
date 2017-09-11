using System.ComponentModel.DataAnnotations;

namespace backend.WebApi.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}