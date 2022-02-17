using Discord;
using Discord.WebSocket;
using Infrastructure.Commands;
using Infrastructure.Services;

namespace DiscordBotWebApi.Bot
{
    public static class DiscordBotInit
    {
        public static IServiceCollection AddClient(this IServiceCollection services, IConfiguration configuration)
        {
            var provider = services.BuildServiceProvider();
            var commandServices = provider.GetService(typeof(CommandServices)) as CommandServices;

            var client = new DiscordSocketClient();
            client.Log += Log;
            client.MessageReceived += new CommandsHandler(client, commandServices).Handler;
            client.UserJoined += new OnMemberJoinHandler(client).MessageSender;
            client.SetGameAsync("Fisting ass in the dungeon");
            client.LoginAsync(TokenType.Bot, configuration.GetValue<String>("BotToken"));
            client.SlashCommandExecuted += new CommandsHandler(client, commandServices).Handler;         
            client.StartAsync();

            while (client.ConnectionState != ConnectionState.Connected) 
            {
            }

            services.AddSingleton(client);

            var setupSlashCommands = provider.GetService(typeof(SetupSlashCommands)) as SetupSlashCommands;
            setupSlashCommands.Execute(client).Wait();
           
            
            return services;
        }

        private static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
