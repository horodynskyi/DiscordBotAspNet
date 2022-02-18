using Discord;
using Discord.WebSocket;
using Infrastructure.Models;

namespace Infrastructure.Commands
{
    public class GetHistoryCommand : DiscordSlashCommand
    {
        public override string Name => "commandhistory";

        public override Task ExecuteAsync(DiscordSocketClient client, object data)
        {
            throw new NotImplementedException();
        }

        public override async Task ExecuteAsync(DiscordSocketClient client, SocketSlashCommand msg)
        {
            using StreamReader r = new("..\\..\\DiscordBotAspNet\\DiscordBotWebApi\\Bot\\CommandHistory.txt");
            var data = r.ReadToEnd();
            var commandList = data.Split('\n');
            data = "";
            for (int i = commandList.Length - 1; i >= 0; i--)
            {
                data += commandList[i];
                if (data.Length + commandList[i].Length > 2000)
                    break;
            }
            await msg.RespondAsync(data);
        }

        public override SlashCommandBuilder GetSlashCommandBuilder()
        {
            return new SlashCommandBuilder
            {
                Name = Name,
                Description = "Send command history"
            };
        }
    }
}
