﻿using Discord.WebSocket;
using System.Collections.ObjectModel;

namespace DiscordBotWebApi.Bot
{
    public class OnMemberJoinHandler
    {
		public readonly IList<String> strings = new ReadOnlyCollection<string>(new List<String> {
			"Легендарный", "Невероянтый", "Жесткий", "Разрывной", ":male_sign:Dungeon Master:male_sign: " });
		private readonly DiscordSocketClient _client;

		public OnMemberJoinHandler(DiscordSocketClient client)
		{
			_client = client;
		}

		public async Task MessageSender(SocketGuildUser user)
		{
			if (_client != null)
			{
				Random random = new Random();
				var channel = _client.GetChannel(874217842650796032) as SocketTextChannel;
				await channel.SendMessageAsync($"Это же тот самый {strings[random.Next(strings.Count)]} {user.Mention} добро пожаловать на {channel.Guild.Name}");
			}
		}
	}
}
