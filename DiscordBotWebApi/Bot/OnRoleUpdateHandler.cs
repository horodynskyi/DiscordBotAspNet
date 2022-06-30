using Discord.WebSocket;
using Infrastructure.Services;
using Models;

namespace DiscordBotWebApi.Bot
{
    public class OnRoleUpdateHandler
    {
        private readonly UserService _userManager;

        public OnRoleUpdateHandler(UserService userManager)
		{
			_userManager = userManager;
        }

		public async Task Handler(SocketRole oldRole, SocketRole newRole)
		{
			await _userManager.UpdateRole(
				new DiscordRole()
				{
					DiscordId = newRole.Id,
					Name = newRole.Name
				}
			);
		}
	}
}
