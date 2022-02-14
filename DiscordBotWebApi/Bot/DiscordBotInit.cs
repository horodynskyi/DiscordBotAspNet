using Discord;
using Discord.WebSocket;

namespace DiscordBotWebApi.Bot
{
    public static class DiscordBotInit
    {
        public static IServiceCollection AddClient(this IServiceCollection services, IConfiguration configuration)
        {
            var client = new DiscordSocketClient();
            client.Log += Log;
            client.MessageReceived += new CommandsHandler(client).Handler;
            client.UserJoined += new OnMemberJoinHandler(client).MessageSender;
            client.SetGameAsync("Fisting ass in the dungeon");
            client.LoginAsync(TokenType.Bot, configuration.GetValue<String>("discordBotToken"));
            client.SlashCommandExecuted += new CommandsHandler(client).Handler;
            client.StartAsync();

            services.AddSingleton(client);

            return services;
        }

        private static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
