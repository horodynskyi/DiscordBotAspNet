using Models;

namespace Infrastructure.Services.Administration
{
	public class GenericGetService
	{
		private readonly UserService _userService;

		public GenericGetService(UserService userService)
		{
			_userService = userService;
		}
		//This metod was created in live share mode by 2 clowns
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

			return result;
		}
	}
}
