using Discord;
using Discord.Net;
using Discord.WebSocket;
using Infrastructure.Models;
using Infrastructure.Services;

namespace Infrastructure.Commands
{
    public class SetupSlashCommands
    {
        private readonly CommandServices _commandServices;

        public SetupSlashCommands(CommandServices commandServices) {
            _commandServices = commandServices;
        }

        public async Task Execute(DiscordSocketClient client)
        {
            var guild = client.GetGuild(873689102569054268);

            var commands = _commandServices._commands;
            commands.Sort((prev, next) => prev.Name.CompareTo(next.Name));
            try
            {
                await guild.DeleteApplicationCommandsAsync();
                var progressStep = 100 / commands.Count;
                var progress = 0;
                foreach (var command in commands)
                {
                    await guild.CreateApplicationCommandAsync(command.GetSlashCommandBuilder().Build());
                    if (progress + progressStep > 100)
                        progress = 100;
                    else
                        progress += progressStep;
                    Console.WriteLine(progress + "%");
                }

            }
            catch (HttpException exception)
            {
                Console.Error.WriteLine(exception.Message);
            }
        }
    }
}
