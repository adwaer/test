using SilentNotary.Common.Query.Criterion.Abstract;

namespace PM.Domain.UserContext.Queries
{
    public class UserTokenQueryCriterion : ICriterion
    {
        public string UserName { get; }
        public string Password { get; }

        public UserTokenQueryCriterion(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}