using Microsoft.AspNetCore.Identity;

namespace CoreLayer.Models
{
    public class UserAppModel : IdentityUser
    {
        public string? City { get; set; }

    }
}
