using Discord.WebSocket;
using Infrastructure.Services;

namespace DiscordBotWebApi.Bot
{
	public class OnRoleDeleteHandler
	{
		private readonly UserService _userManager;

		public OnRoleDeleteHandler(UserService userManager)
		{
			_userManager = userManager;
		}

		public async Task Handler(SocketRole role)
		{
			await _userManager.RemoveRole(role.Id);
		}
	}
}
