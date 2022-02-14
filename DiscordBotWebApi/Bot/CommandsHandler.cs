using Discord.WebSocket;
using Interfaces;
using Services;
using Services.Commands;
using Services.Models;

namespace DiscordBotWebApi.Bot
{
    public class CommandsHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandServices commandServices = new CommandServices();

        public CommandsHandler(DiscordSocketClient client)
        {
            _client = client;
        }

        public Task Handler(SocketSlashCommand commandData)
        {
            var command = commandServices.GetComand(commandData);

            if (command != null) 
            {
                command.Execute(_client, commandData);
                WriteToHistory($"User {commandData.User.Username} run command named **{command.Name}**");
            }

            return Task.CompletedTask;
        }

        public Task Handler(SocketMessage msg)
        {
            var command = commandServices.GetComand(msg);

            if (command != null)
            {
                command.Execute(_client, msg);
                WriteToHistory($"User {msg.Author.Username} run command named **{command.Name}**");
            }

            return Task.CompletedTask;
        }

        public void Handler(ICommand command, object data)
        {
            if (command != null) 
            {
                switch (command)
                {
                    case SendImageCommand: command.Execute(_client, data); break;
                }
                WriteToHistory($"User ADMIN run command from API named **{command.Name}** + Command data = {data}no_link");
            }
        }

        private void WriteToHistory(string message)
        {
            using (var writer = new StreamWriter("..\\..\\DiscordBotAspNet\\DiscordBotWebApi\\Bot\\CommandHistory.txt", true, System.Text.Encoding.Default))
            {
                writer.WriteLine(DateTime.Now.ToString("dd/MM/yyyy:H:m") + $" - {message}");
            }   
        }
    }
}
