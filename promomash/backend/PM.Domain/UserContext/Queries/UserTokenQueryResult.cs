namespace PM.Domain.UserContext.Queries
{
    public class UserTokenQueryResult
    {
        public string Token { get; }

        public UserTokenQueryResult(string token)
        {
            Token = token;
        }
    }
}