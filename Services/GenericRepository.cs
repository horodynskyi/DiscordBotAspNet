using Ardalis.Specification.EntityFrameworkCore;
using Infrastructure.Database;
using Interfaces;

namespace Infrastructure
{
    public class GenericRepository<T> : RepositoryBase<T>, IRepository<T> where T : class
    {
        public GenericRepository(DiscordBotContext dbContext) : base(dbContext)
        {
        }
    }
}
