using m = Discord;

namespace Models
{
    public class DiscordUser : m.IEntity<ulong>
    {
        public ulong Id { get; set; }
        public string DiscordId { get; set; }
        public string GuildId { get; set; }
        public string Name { get; set; } 
        public int PrestigeLevel { get; set; }  
    }
}