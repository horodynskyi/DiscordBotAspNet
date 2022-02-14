using System.Text.Json;

namespace Services
{
    public class AdminService
    {
        public static void SetAdminData(string id)
        {
            var jsonData = new Dictionary<string, string>
            {
                { "id", id }
            };
            string json = JsonSerializer.Serialize(jsonData);
            File.WriteAllText(@"..\\..\\DiscordBotAspNet\\DiscordBotWebApi\\Bot\\Settings\\admin.json", json);
        }

        public static async Task<string> GetAdminDataAsync()
        {
            using StreamReader r = new("..\\..\\DiscordBotAspNet\\DiscordBotWebApi\\Bot\\Settings\\admin.json");
            var jsonData = await JsonSerializer.DeserializeAsync<Dictionary<string, string>>(r.BaseStream);
            return jsonData["id"] ?? "";
        }
    }
}
