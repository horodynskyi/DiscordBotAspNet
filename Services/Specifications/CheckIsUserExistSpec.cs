using Ardalis.Specification;
using Models;

namespace Infrastructure.Specifications
{
    public class CheckIsUserExistSpec : Specification<DiscordUser>, ISingleResultSpecification<DiscordUser>
    {
        public CheckIsUserExistSpec(string DiscordId)
        {
            Query.Where(x => x.DiscordId == DiscordId);
        }
    }
}
