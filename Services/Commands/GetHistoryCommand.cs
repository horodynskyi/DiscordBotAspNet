using Discord.WebSocket;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.Commands
{
    public class GetHistoryCommand : DiscordComand
    {
        public override string Name => "command history";

        public override void Execute(DiscordSocketClient client, object data)
        {
            throw new NotImplementedException();
        }

        public override async void ExecuteAsync(DiscordSocketClient client, SocketSlashCommand msg)
        {
            using (StreamReader r = new StreamReader("..\\..\\DiscordBotAspNet\\DiscordBotWebApi\\Bot\\CommandHistory.txt"))
            {
                var data = r.ReadToEnd();
                await msg.Channel.SendMessageAsync(data);
            }           
        }
    }
}
