using Discord;
using Discord.WebSocket;
using Infrastructure.Models;
using System.Text;

namespace Infrastructure.Commands
{
    public class GetHistoryCommand : DiscordSlashCommand
    {
        public override string Name => "commandhistory";

        public override async Task ExecuteAsync(DiscordSocketClient client, object commandObj)
        {
            if (commandObj is SocketSlashCommand command) 
            {
                using StreamReader r = new("..\\..\\DiscordBotAspNet\\DiscordBotWebApi\\Bot\\Logs\\CommandHistory.txt");
                var data = new StringBuilder(r.ReadToEnd());

                var commandList = data.ToString().Split('\n');
                data.Clear();
                for (int i = commandList.Length - 1; i >= 0; i--)
                {
                    data.Append(commandList[i]);
                    if (data.Length + commandList[i].Length > 2000)
                        break;
                }
                await command.RespondAsync(data.ToString());
            } 
            else 
            {
                return;
            }
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
