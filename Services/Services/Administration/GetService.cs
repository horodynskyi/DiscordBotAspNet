using AutoMapper;
using Discord.WebSocket;
using DTOModels;
using Models;

namespace Infrastructure.Services.Administration
{
	public class GetService
	{
		private readonly UserService _userService;
		private readonly DiscordSocketClient _client;
		private readonly IMapper _mapper;

		public GetService(UserService userService, DiscordSocketClient client, IMapper mapper)
		{
			_userService = userService;
			_client = client;
			_mapper = mapper;
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
			else if (type == typeof(SocketGuild))
			{
				foreach (var guild in _client.Guilds)
				{
					result.Add(_mapper.Map<AdministrationGuildDTO>(guild));
				}
			}

			return result;
		}
	}
}
