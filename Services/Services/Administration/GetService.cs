using Models;

namespace Infrastructure.Services.Administration
{
	public class GetService
	{
		private readonly UserService _userService;

		public GetService(UserService userService)
		{
			_userService = userService;
		}

		public async Task<List<object>> GetAllAsync(Type type)
		{
			var result = new List<object>();
			if (type == typeof(DiscordUser))
			{
				foreach (var user in await _userService.GetAllUsers())
				{
					result.Add(user);
				}
			} 
			else if (type == typeof(DiscordRole))
			{
				foreach (var role in await _userService.GetAllRoles())
				{
					result.Add(role);
				}
			}

			return result;
		}
	}
}
