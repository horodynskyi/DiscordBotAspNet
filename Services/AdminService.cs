using System.Text.Json;

namespace Services
{
    public class AdminService
    {
        public void SetAdminData(string id)
        {
            var jsonData = new Dictionary<string, string>();
            jsonData.Add("id", id);
            string json = JsonSerializer.Serialize(jsonData);
            File.WriteAllText(@"..\\..\\DiscordBotAspNet\\DiscordBotWebApi\\Bot\\Settings\\admin.json", json);

        }

        public async Task<string> GetAdminDataAsync()
        {
            using (StreamReader r = new StreamReader("..\\..\\DiscordBotAspNet\\DiscordBotWebApi\\Bot\\Settings\\admin.json"))
            {
                var jsonData = await JsonSerializer.DeserializeAsync<Dictionary<string, string>>(r.BaseStream);
                return jsonData["id"] ?? "";
            }           
        }
    }
}
