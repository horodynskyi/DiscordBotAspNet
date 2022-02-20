using Discord;
using Discord.WebSocket;
using Infrastructure.Database;
using Models;

namespace Infrastructure.Services
{    
    public class UserService
    {
        private readonly DiscordBotContext _discordBotContext;
        public UserService(DiscordBotContext discordBotContext)
        {
            _discordBotContext = discordBotContext;
        }

        public async Task FetchAllUsersFromDiscord(DiscordSocketClient discordSocketClient) 
        {
            var guild = discordSocketClient.GetGuild(873689102569054268) as IGuild;
            var users = await guild.GetUsersAsync();
            foreach (var user in users.Where(x => !x.IsBot))
            {
                var tmpUser = new DiscordUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = user.Username,
                    PrestigeLevel = 0
                };
                await AddUser(tmpUser);
            }
            var res = GetAllUsers();
            res.ToString();
        }

        public async Task AddUser(DiscordUser discordUser) 
        {
            await _discordBotContext.Users.AddAsync(discordUser);
            await _discordBotContext.SaveChangesAsync();
        }

        public List<DiscordUser> GetAllUsers()
        {
            return _discordBotContext.Users.ToList();
        }
    }
}
