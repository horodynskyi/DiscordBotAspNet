using Microsoft.EntityFrameworkCore;
using Models;

namespace Infrastructure.Database
{
    public class DiscordBotContext : DbContext
    {
        public DiscordBotContext() { }
        public DiscordBotContext(DbContextOptions<DiscordBotContext> options) : base(options) { }
        public DbSet<DiscordUser> Users => Set<DiscordUser>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<UserRoles> UserRoles => Set<UserRoles>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DiscordUser>().Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
