using Microsoft.EntityFrameworkCore;
using Models;

namespace Infrastructure.Database
{
    public class DiscordBotContext : DbContext
    {
        public DiscordBotContext() { }
        public DiscordBotContext(DbContextOptions<DiscordBotContext> options) : base(options) { }
        public DbSet<DiscordUser> Users => Set<DiscordUser>();
        public DbSet<DiscordRole> Roles => Set<DiscordRole>();
        public DbSet<UserRoles> UserRoles => Set<UserRoles>();
        public DbSet<Administrator> Administrators => Set<Administrator>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DiscordUser>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<DiscordRole>().HasKey(x => x.DiscordId);
            modelBuilder.Entity<DiscordRole>().Property(x => x.DiscordId).ValueGeneratedNever();
        }
    }
}
