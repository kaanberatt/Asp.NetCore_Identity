using Asp.NetCore_Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Asp.NetCore_Identity.Context
{
    public class IdentityContext:IdentityDbContext<AppUser,AppRole,int>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {

        }
    }
}
