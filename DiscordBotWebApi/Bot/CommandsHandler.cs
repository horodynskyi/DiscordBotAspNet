using Discord.WebSocket;
using Infrastructure.Commands;
using Infrastructure.Models;
using Infrastructure.Services;
using Interfaces;

namespace DiscordBotWebApi.Bot
{
    public class CommandsHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commandServices;
        private readonly UserService _userService;

        public CommandsHandler(DiscordSocketClient client, CommandService commandServices, UserService userService)
        {
            _client = client;
            _commandServices = commandServices;
            _userService = userService;
        }

        public async Task Handler(SocketSlashCommand commandData)
        {
            if (!commandData.User.IsBot) 
            {
                await _userService.AddPointsFotUser(commandData.User.Id.ToString(), 1);
                var command = _commandServices.GetComand(commandData);

                if (command != null)
                {
                    try
                    {
                        await command.ExecuteAsync(_client, commandData);
                        WriteToHistory($"User {commandData.User.Username} run command named **{command.Name}**");
                    }
                    catch (Exception e)
                    {
                        await commandData.RespondAsync("An unexpected error occured");
                        WriteToHistory($"Error while user {commandData.User.Username} try to call command named **{command.Name}** - command error -> {e}");
                    }
                }
                else
                {
                    await commandData.RespondAsync("Command not found");
                    WriteToHistory($"Error while user {commandData.User.Username} try to call command named **{commandData.CommandName}** - command not found");
                }
            }
        }

        public async Task Handler(SocketMessage msg)
        {
            await _userService.AddPointsFotUser(msg.Author.Id.ToString(), 1);

            var command = _commandServices.GetComand(msg);

            if (!msg.Author.IsBot)
            {
                if (command != null)
                {
                    try
                    {
                        await command.ExecuteAsync(_client, msg);
                        WriteToHistory($"User {msg.Author.Username} run command named **{command.Name}**");
                    }
                    catch (Exception e)
                    {
                        await msg.Channel.SendMessageAsync("An unexpected error occured");
                        WriteToHistory($"Error while user {msg.Author.Username} try to call command named **{command.Name}** - command error -> {e}");
                    }

                }
                else if (msg.Content.StartsWith('!'))
                {
                    await msg.Channel.SendMessageAsync("Command not found");
                    WriteToHistory($"Error while user {msg.Author.Username} try to call command named **{msg.Content}** - command not found");
                }
            }
        }

        private static void WriteToHistory(string message)
        {
            var pathToHistory = "..\\DiscordBotWebApi\\Bot\\Logs";
            if (!Directory.Exists(pathToHistory))
            Directory.CreateDirectory(pathToHistory);
            using var writer = new StreamWriter(pathToHistory + "\\CommandHistory.txt", true, System.Text.Encoding.Default);
            writer.WriteLine(DateTime.Now.ToString("dd/MM/yyyy:H:m") + $" - {message}");
        }
    }
}
