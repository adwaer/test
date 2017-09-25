using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using backend.Domain.Entities;
using backend.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace backend.WebApi.Middlewares
{
    /// <summary>
    /// auth middleware
    /// </summary>
    public class AuthMiddleware : OwinMiddleware
    {
        private const string Realm = "AngularWebAPI";

        /// <summary>
        /// default ctor
        /// </summary>
        /// <param name="next"></param>
        public AuthMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        /// <inheritdoc />
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
                    var userManager = context.GetUserManager<CustomUserManager>();
                    await AuthenticateUser(authHeaderVal.Parameter, userManager);
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

        private async Task<bool> AuthenticateUser(string credentials, CustomUserManager userManager)
        {
            var encoding = Encoding.GetEncoding("iso-8859-1");
            credentials = encoding.GetString(Convert.FromBase64String(credentials));

            var credentialsArray = credentials.Split(':');
            var username = credentialsArray[0];
            var password = credentialsArray[1];

            return await SetCreds(credentials, username, password, userManager);
        }

        private static async Task<bool> SetCreds(string credentials, string username, string password,
            CustomUserManager userManager)
        {
            ApplicationUser account = await userManager.FindAsync(username, password);
            if (account == null)
            {
                return false;
            }

            var identity = new GenericIdentity(account.Id);

            SetPrincipal(new GenericPrincipal(identity, new string[0]));

            return true;
        }
    }
}