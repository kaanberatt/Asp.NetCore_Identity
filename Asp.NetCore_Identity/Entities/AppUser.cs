using Microsoft.AspNetCore.Identity;

namespace Asp.NetCore_Identity.Entities
{
    public class AppUser: IdentityUser<int>
    {
        public string ImagePath { get; set; }
        public string Gender { get; set; }

    }
}
