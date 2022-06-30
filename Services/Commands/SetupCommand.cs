using Discord;
using Discord.Net;
using Discord.WebSocket;
using Infrastructure.Models;
using Infrastructure.Services;

namespace Infrastructure.Commands
{
    public class SetupCommand : DiscordMessageCommand
    {
        private readonly CommandService _commandService;
        private readonly UserService _userService;

        public SetupCommand(CommandService commandService, UserService userService)
        {
            _commandService = commandService;
            _userService = userService;
        }

        public override string Name => "!setup";

        public override async Task ExecuteAsync(DiscordSocketClient client, object commandObj)
        {
            if (commandObj is SocketMessage message)
            {
                if (message.Content.Contains("!setup")) 
                {
                    var paramets = message.Content.Remove(0, 6).Split(" ");
                    if (paramets.Contains("all"))
                    {
                        await SetupSlashCommands(client, commandObj as SocketMessage);
                        await _userService.FetchAllUsersFromDiscord(client);
                        await _userService.FetchAllRolesFromDiscord(client);
                        await _userService.RemoveUsersStatsChannel(client);
                        await _userService.GeneratePersonalStatisticChats(client);
                    } 
                    else if (paramets.Contains("users")) 
                    {
                        await _userService.FetchAllUsersFromDiscord(client);
                        await _userService.FetchAllRolesFromDiscord(client);
                        await _userService.RemoveUsersStatsChannel(client);
                        await _userService.GeneratePersonalStatisticChats(client);
                    }
                    //Todo make stats comand
                    else if (paramets.Contains("stats") && paramets.Contains("delete"))
                    {
                        await _userService.RemoveUsersStatsChannel(client);
                    }
                    else if (paramets.Contains("slashcommands"))
                    {
                        await SetupSlashCommands(client, commandObj as SocketMessage);
                    }
                }
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
                Description = "Setup commands"
            };
        }

        private async Task SetupSlashCommands(DiscordSocketClient client, SocketMessage message) 
        {
            var guild = client.GetGuild(873689102569054268);

            var commands = _commandService.Commands.Where(x => x is DiscordSlashCommand).ToList();
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
