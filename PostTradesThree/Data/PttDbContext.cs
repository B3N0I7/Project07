using Microsoft.EntityFrameworkCore;
using PostTradesThree.Domain;

namespace PostTradesThree.Data
{
    public class PttDbContext : DbContext
    {
        public PttDbContext(DbContextOptions<PttDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Bid>();
            modelBuilder.Entity<CurvePoint>();
            modelBuilder.Entity<Rating>();
            modelBuilder.Entity<RuleName>();
            modelBuilder.Entity<Trade>();
        }

        public DbSet<Bid> Bids { get; set; }
        public DbSet<CurvePoint> CurvePoints { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<RuleName> RuleNames { get; set; }
        public DbSet<Trade> Trades { get; set; }
    }
}
