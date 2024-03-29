﻿using System.Text.Json;

namespace Infrastructure.Services
{
    public class DanbooruService
    {
        private readonly HttpClient _httpClient = new();   

        public async Task<String> GetRandomArt(bool cencorship = false) 
        {
            string domain;

            if (cencorship)
                domain = "https://danbooru.donmai.us/";
            else
                domain = "https://safebooru.donmai.us/";

            Console.WriteLine("cens = " + !cencorship + " domain " + domain);
            var res = await _httpClient.GetAsync(domain + "posts/random.json");
            var data = JsonSerializer.Deserialize<Dictionary<string, object>>(await res.Content.ReadAsStringAsync());
            var fileUrlDictionary = data.FirstOrDefault(x => x.Key == "file_url");
            var ifNotExistUrl = fileUrlDictionary.Value == null;
            Console.WriteLine("ifNotExistUrl = " + ifNotExistUrl);
            while (ifNotExistUrl) 
            {
                return null;
            }

            return data["file_url"].ToString();
        }

        public async Task<String> GetArt(String tags, bool cencorship = false)
        {
            String domain;
            if (cencorship)
                domain = "https://danbooru.donmai.us/";
            else
                domain = "https://safebooru.donmai.us/";

            Console.WriteLine("cens = " + !cencorship + " domain " + domain);
            var res = await _httpClient.GetAsync(domain + $"posts/random.json?tags={tags}");
            var data = JsonSerializer.Deserialize<Dictionary<string, object>>(await res.Content.ReadAsStringAsync());
            var fileUrlDictionary = data.FirstOrDefault(x => x.Key == "file_url");
            var ifNotExistUrl = fileUrlDictionary.Value == null;
            Console.WriteLine("ifNotExistUrl = " + ifNotExistUrl);
            while (ifNotExistUrl)
            {
                return null;
            }

            return data["file_url"].ToString();
        }
    }
}
