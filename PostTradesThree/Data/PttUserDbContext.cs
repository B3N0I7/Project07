using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PostTradesThree.Domain;

namespace PostTradesThree.Data
{
    public class PttUserDbContext : IdentityDbContext<PttUser>
    {
        public PttUserDbContext(DbContextOptions<PttUserDbContext> options) : base(options)
        {
        }
    }
}
