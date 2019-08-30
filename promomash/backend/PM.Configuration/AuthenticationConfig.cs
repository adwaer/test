namespace PM.Configuration
{
    public class AuthenticationConfig
    {
        public string Url { get; set; }
        public string SecretJwtKey { get; set; }
        public int LifeTime { get; set; }
    }
}
