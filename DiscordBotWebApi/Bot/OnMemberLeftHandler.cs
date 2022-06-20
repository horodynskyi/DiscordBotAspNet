using Discord.WebSocket;
using Infrastructure.Services;

namespace DiscordBotWebApi.Bot
{
    public class OnMemberLeftHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly UserService _userService;

        public OnMemberLeftHandler(DiscordSocketClient client, UserService userService)
        {
            _client = client;
            _userService = userService;
        }

		public async Task Handler(SocketGuild guild, SocketUser user)
		{
			if (_client != null)
			{
				var channel = _client.GetChannel(942780457232257044) as SocketTextChannel;

                await _userService.RemoveUserStatsChannel(user, _client);

                await channel.SendMessageAsync($"ББ даун {user.Mention}");
			}
		}
	}
}
