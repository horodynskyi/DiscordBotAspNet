using Discord;
using Discord.WebSocket;
using Infrastructure.Services;
using System.Collections.ObjectModel;

namespace DiscordBotWebApi.Bot
{
    public static class DiscordBotInit
    {
        static readonly DiscordSocketClient _client = new(new DiscordSocketConfig() { 
            AlwaysDownloadUsers = true,
            GatewayIntents = GatewayIntents.All,
            
        });
        private static ServiceProvider _provider;

        public static IServiceCollection AddClient(this IServiceCollection services, IConfiguration configuration)
        {
            _provider = services.BuildServiceProvider();
            var commandServices = _provider.GetService(typeof(CommandService)) as CommandService;
        
            _client.Log += Log;
            _client.MessageReceived += new CommandsHandler(_client, commandServices).Handler;
            _client.SlashCommandExecuted += new CommandsHandler(_client, commandServices).Handler;
            _client.UserJoined += new OnMemberJoinHandler(_client).MessageSender;
            _client.SetGameAsync("Fisting ass in the dungeon");
            _client.LoginAsync(TokenType.Bot, configuration.GetValue<String>("BotToken"));
            _client.Ready += OnReady;
            _client.StartAsync();
            services.AddSingleton(_client);

            while (_client.ConnectionState != ConnectionState.Connected) 
            {           

            }                   

            var setupSlashCommands = _provider.GetService(typeof(SetupSlashCommands)) as SetupSlashCommands;
            setupSlashCommands.Execute(_client).Wait();
                  
            return services;
        }

        private static async Task OnReady()
        {
            var userServices = _provider.GetService(typeof(UserService)) as UserService;
            await userServices.FetchAllUsersFromDiscord(_client);
        }

        private static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
