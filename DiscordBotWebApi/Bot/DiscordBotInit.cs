using Discord;
using Discord.WebSocket;
using Services.Commands;
using System.Text.Json;

namespace DiscordBotWebApi.Bot
{
    public static class DiscordBotInit
    {
        private static DiscordSocketClient _client { get; set; }
        public static IServiceCollection AddClient(this IServiceCollection services, IConfiguration configuration)
        {
            var client = new DiscordSocketClient();
            _client = client;
            client.Log += Log;
            //client.MessageReceived += notSlashCommand;
            client.UserJoined += new OnMemberJoinHandler(client).MessageSender;
            client.SetGameAsync("Fisting ass in the dungeon");
            client.LoginAsync(TokenType.Bot, configuration.GetValue<String>("discordBotToken"));
            client.SlashCommandExecuted += new CommandsHandler(client).Handler;
            client.StartAsync();

            services.AddSingleton(client);

            return services;
        }

        private async static Task notSlashCommand(SocketMessage msg)
        {
            var z = new SetupSlashCommands();
            z.Execute(_client, msg);
        }

        private static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
