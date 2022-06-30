using Discord.WebSocket;
using Infrastructure.Services;
using Models;

namespace DiscordBotWebApi.Bot
{
	public class OnRoleCreatedHandler
	{
		private readonly UserService _userManager;

		public OnRoleCreatedHandler(UserService userManager)
		{
			_userManager = userManager;
		}

		public async Task Handler(SocketRole role)
		{
			await _userManager.AddRole(
				new DiscordRole() { 
					DiscordId = role.Id,
					Name = role.Name
				}
			);
		}	
	}
}
