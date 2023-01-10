using Microsoft.AspNetCore.Identity;
using System;

namespace Asp.NetCore_Identity.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public DateTime CreatedDate { get; set; }
    }
}
