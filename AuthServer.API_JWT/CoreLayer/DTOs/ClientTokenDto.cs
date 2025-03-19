namespace CoreLayer.DTOs
{
    public class ClientTokenDto
    {
        public string AccessToken { get; set; } = default!;
        public DateTime AccessTokenExpiration { get; set; }
    }
}
