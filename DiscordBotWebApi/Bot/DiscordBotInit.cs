using Discord;
using Discord.WebSocket;
using Infrastructure.Services;

namespace DiscordBotWebApi.Bot
{
    public static class DiscordBotInit
    {
        public static IServiceCollection AddClient(this IServiceCollection services, IConfiguration configuration)
        {   
            DiscordSocketClient client = new(new DiscordSocketConfig()
            {
                AlwaysDownloadUsers = true,
                GatewayIntents = GatewayIntents.All,

            });

            var provider = services.BuildServiceProvider();
            var commandService = provider.GetService(typeof(CommandService)) as CommandService;
            var userService = provider.GetService(typeof(UserService)) as UserService;

            client.Log += Log;
            client.MessageReceived += new CommandsHandler(client, commandService, userService).Handler;
            client.SlashCommandExecuted += new CommandsHandler(client, commandService, userService).Handler;
            client.UserJoined += new OnMemberJoinHandler(client, userService).MessageSender;
            client.UserLeft += new OnMemberLeftHandler(client, userService).Handler;
            client.RoleCreated += new OnRoleCreatedHandler(userService).Handler;
            client.RoleDeleted += new OnRoleDeleteHandler(userService).Handler;
            client.RoleUpdated += new OnRoleUpdateHandler(userService).Handler;
            client.Ready += OnReadyAsync;

            client.SetGameAsync("Refactoring facebook API");
            client.LoginAsync(TokenType.Bot, configuration.GetValue<String>("BotToken"));
            client.StartAsync();
            services.AddSingleton(client);

            return services;
        }

        private static Task OnReadyAsync()
        {
            return Task.CompletedTask;
        }
        private static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
