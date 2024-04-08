using Microsoft.AspNetCore.Identity;

namespace Diplom.Data
{
    public class AppUser : IdentityUser
    {
        public string City { get; set; }
        public string Country { get; set; }
    }
}
