using System;
using System.Web.Http;
using backend.WebApi.ViewModels;

namespace backend.WebApi.Controllers
{
    /// <summary>
    /// Login api
    /// </summary>
    public class LoginController : ApiController
    {
        public const string Login = "test1";
        public const string Pwd = "test2";

        /// <summary>
        /// Validate login password pair
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IHttpActionResult Post(LoginViewModel model)
        {
            if (model.Login.Equals(Login, StringComparison.CurrentCultureIgnoreCase)
                && model.Password.Equals(Pwd))
            {
                return Ok();
            }

            return BadRequest("Неверная комбинация логина и пароля");
        }
    }
}
