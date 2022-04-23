using Discord;
using Discord.WebSocket;
using Infrastructure.Database;
using Infrastructure.Specifications;
using Interfaces;
using Models;

namespace Infrastructure.Services
{    
    public class UserService
    {
        private readonly IRepository<DiscordUser> _repository;

        public UserService(IRepository<DiscordUser> repository)
        {
            _repository = repository;
        }

        public async Task FetchAllUsersFromDiscord(DiscordSocketClient discordSocketClient) 
        {
            var guild = discordSocketClient.GetGuild(873689102569054268) as IGuild;
            var users = await guild.GetUsersAsync();
            foreach (var user in users.Where(x => !x.IsBot))
            {
                var tmpUser = new DiscordUser
                {
                    Name = user.Username,
                    PrestigeLevel = 0,
                    GuildId = user.GuildId.ToString(),
                    DiscordId = user.Id.ToString()
                };

                await AddUser(tmpUser);
            }
        }

        public async Task AddUser(DiscordUser discordUser) 
        {
            if (await _repository.GetBySpecAsync(new CheckIsUserExistSpec(discordUser.DiscordId)) == null)
            {
                await _repository.AddAsync(discordUser);
            }
        }

        public async Task<int> AddPointsFotUser(string userId, int pointsCount)
        {
            var tmpUser = await _repository.GetBySpecAsync(new CheckIsUserExistSpec(userId));
            if (tmpUser != null)
            {
                tmpUser.PrestigeLevel += pointsCount;
                await _repository.UpdateAsync(tmpUser);
            }

            if(tmpUser != null)
                return tmpUser.PrestigeLevel;
            else
                return 0;
        }

        public async Task<List<DiscordUser>> GetAllUsers()
        {
            return await _repository.ListAsync();
        }
    }
}
