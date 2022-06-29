using Ardalis.Specification;
using Models;

namespace Infrastructure.Specifications
{
	public class CheckIsRoleExistSpec : Specification<DiscordRole>, ISingleResultSpecification<DiscordRole>
    {
        public CheckIsRoleExistSpec(ulong DiscordId)
        {
            Query.Where(x => x.DiscordId == DiscordId);
        }
    }
}
