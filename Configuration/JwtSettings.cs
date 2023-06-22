namespace WebAPI.Configuration
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public int ExpirationMinutes { get; set; }
    }
}
