using Discord;
using Discord.WebSocket;
using Infrastructure.Models;
using System.Diagnostics;

namespace Infrastructure.Commands
{
    public class JoinToVoiceChat : DiscordSlashCommand
    {
        public override string Name => "jointochat";

        public override async Task ExecuteAsync(DiscordSocketClient client, object commandObj)
        {
            if (commandObj is SocketSlashCommand command)
            {
                var user = command.User as SocketGuildUser;
                var channel = user.VoiceChannel as IVoiceChannel;
                var m = await channel.ConnectAsync();
                
            }
            else
            {
                return;
            }
        }

        private Process CreateStream(string path)
        {
            return Process.Start(new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = $"-hide_banner -loglevel panic -i \"{path}\" -ac 2 -f s16le -ar 48000 pipe:1",
                UseShellExecute = false,
                RedirectStandardOutput = true,
            });
        }

        public override SlashCommandBuilder GetSlashCommandBuilder()
        {
            return new SlashCommandBuilder
            {
                Name = Name,
                Description = "Clear channel"
            };
        }
    }
}
