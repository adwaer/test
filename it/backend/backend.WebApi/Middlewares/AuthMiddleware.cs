using System;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using backend.WebApi.Controllers;
using Microsoft.Owin;

namespace backend.WebApi.Middlewares
{
    public class AuthMiddleware : OwinMiddleware
    {
        private const string Realm = "AngularWebAPI";

        public AuthMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            var authHeader = context.Request.Headers["auth"];
            if (authHeader != null)
            {
                var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);

                // RFC 2617 sec 1.2, "scheme" name is case-insensitive
                if (authHeaderVal.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) &&
                    authHeaderVal.Parameter != null)
                {
                    AuthenticateUser(authHeaderVal.Parameter);
                }
            }

            var response = HttpContext.Current.Response;
            if (response.StatusCode == 401)
            {
                response.Headers.Add("WWW-Authenticate", $"Basic realm=\"{Realm}\"");
            }

            await Next.Invoke(context);
        }

        private static void SetPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }

        private bool AuthenticateUser(string credentials)
        {
            var encoding = Encoding.GetEncoding("iso-8859-1");
            credentials = encoding.GetString(Convert.FromBase64String(credentials));

            return SetCreds(credentials);
        }

        private static bool SetCreds(string credentials)
        {
            var credentialsArray = credentials.Split(':');
            var username = credentialsArray[0];
            var password = credentialsArray[1];

            if (username.Equals(LoginController.Login, StringComparison.CurrentCultureIgnoreCase)
                && password.Equals(LoginController.Pwd))
            {
                SetPrincipal(new GenericPrincipal(new GenericIdentity(username), new string[0]));
                return true;
            }

            SetPrincipal(new GenericPrincipal(new GenericIdentity(username), new string[0]));

            return false;
        }
    }
}