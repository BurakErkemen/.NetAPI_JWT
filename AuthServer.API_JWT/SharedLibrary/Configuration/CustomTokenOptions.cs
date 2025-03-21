namespace SharedLibrary.Configuration
{
    public class CustomTokenOptions
    {
        public List<string> Audience { get; set; } = default!;
        public string Issuer { get; set; } = default!;

        public int AccessTokenExpiration { get; set; }
        public int RefreshTokenExpiration { get; set; }

        public string SecurityKey { get; set; } = default!;
    }
}