using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Services.BackgroundServices
{
	public class UpdateUserStatisticHostedServices : IHostedService, IDisposable
	{

        private Timer _timer = null;
		private readonly DiscordSocketClient _client;
        private readonly IServiceProvider _serviceProvider;

        public UpdateUserStatisticHostedServices(IServiceProvider serviceProvider, DiscordSocketClient client) 
        {
			_client = client;
            _serviceProvider = serviceProvider;

        }

        private async void UpdateStatistic(object state)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                if (_client.ConnectionState == ConnectionState.Connected)
                {
					try
					{
                        var userService =
                        scope.ServiceProvider.GetRequiredService<UserService>();
                        var users = (await userService.GetAllUsers()).OrderBy(x => x.Name).ToList();
                        var guild = _client.GetGuild(873689102569054268) as IGuild;
                        var userChannels = (await guild.GetTextChannelsAsync()).Where(x => x.Name.Contains("stats")).OrderBy(x => x.Name).ToList();

                        users.ForEach(async (user) =>
                        {
                            var userChannel = userChannels.FirstOrDefault(x => x.Name.Replace("-stats", "").Replace("-", " ") == user.Name.ToLower());

                            var lastMessage = (await userChannel.GetMessagesAsync().LastAsync()).Cast<IMessage>().First();
                            await lastMessage.DeleteAsync();

                            await userChannel.SendMessageAsync($" Ваш поточний рівень на сервері - {user.PrestigeLevel / 100}\nВаша поточна кількість очок - {user.PrestigeLevel}");
                        });
                    }
					catch (Exception e)
					{
                        Console.WriteLine("Error in update user statistic background service - " + e);
						throw;
					}
                }
                else
                {
                    Console.WriteLine("Client not connected");
                }
            }
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(UpdateStatistic, null, TimeSpan.Zero,
                TimeSpan.FromHours(1));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
