namespace CoreLayer.Models
{
    public class UserRefreshToken
    {
        public string UserId { get; set; } = default!;
        public string Token { get; set; } = default!;
        public DateTime Expiration { get; set; }
    }
}