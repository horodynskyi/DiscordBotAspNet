using Discord;
using Discord.WebSocket;
using Infrastructure.Services;
using System.Collections.ObjectModel;

namespace DiscordBotWebApi.Bot
{
    public static class DiscordBotInit
    {
        public static readonly IList<String> strings = new ReadOnlyCollection<string>(new List<String> {
            "Легендарный", "Невероянтый", "Жесткий", "Разрывной", ":male_sign:Dungeon Master:male_sign: " });
        static readonly DiscordSocketClient _client = new();

        public static IServiceCollection AddClient(this IServiceCollection services, IConfiguration configuration)
        {
            var provider = services.BuildServiceProvider();
            var commandServices = provider.GetService(typeof(CommandService)) as CommandService;

            
            _client.Log += Log;
            _client.MessageReceived += new CommandsHandler(_client, commandServices).Handler;
            _client.UserJoined += MessageSender;
            _client.SetGameAsync("Fisting ass in the dungeon");
            _client.LoginAsync(TokenType.Bot, configuration.GetValue<String>("BotToken"));
            _client.SlashCommandExecuted += new CommandsHandler(_client, commandServices).Handler;
            _client.StartAsync();

            while (_client.ConnectionState != ConnectionState.Connected) 
            {           

            }
            services.AddSingleton(_client);

            var setupSlashCommands = provider.GetService(typeof(SetupSlashCommands)) as SetupSlashCommands;
            setupSlashCommands.Execute(_client).Wait();
                  
            return services;
        }

        public static Task MessageSender(SocketGuildUser user)
        {
            if (_client != null)
            {
                Random random = new();
                var channel = _client.GetChannel(942780457232257044) as SocketTextChannel;
                channel.SendMessageAsync($"Это же тот самый {strings[random.Next(strings.Count)]} {user.Mention} добро пожаловать на {channel.Guild.Name}");
            }
            return Task.CompletedTask;
        }

        private static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
