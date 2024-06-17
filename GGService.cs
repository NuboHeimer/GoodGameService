using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

///----------------------------------------------------------------------------
///   Module:       GoodGame Service
///   Author:       NuboHeimer (https://vkplay.live/nuboheimer)
///   Email:        nuboheimer@yandex.ru
///   Telegram:     t.me/nuboheimer
///   Version:      0.0.0
///----------------------------------------------------------------------------
public class CPHInline
{
    private readonly HttpClient Client = new();

    public bool Execute()
    {
        // Path to your JSON file
        string miniChatSettingsPath = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\StreaMix\\MiniChat\\Settings.json";
        // Read the JSON file
        string miniChatSettings = File.ReadAllText(miniChatSettingsPath);
        CPH.LogInfo(miniChatSettings);
        // Deserialize the JSON string to a custom class
        var data = JsonConvert.DeserializeObject<MiniChatSettings>(miniChatSettings);
        // Access the data
        string goodGameChannelKey = data.GoodGame.ChannelKey;
        CPH.LogInfo("GoodGame Channel Key: " + goodGameChannelKey);
        string AccessToken = data.GoodGame.AccessToken;
        CPH.LogInfo("GoodGame Access Token: " + AccessToken);
        using HttpResponseMessage response = Client.GetAsync("https://goodgame.ru/api/4/streams/2/channel/NuboHeimer").GetAwaiter().GetResult();
        response.EnsureSuccessStatusCode();
        string responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        CPH.LogInfo(responseBody);
        return true;
    }

    public class MiniChatSettings
    {
        public GoodGameData GoodGame { get; set; }
    }

    public class GoodGameData
    {
        public string ChannelKey { get; set; }
        public string AccessToken { get; set; }
    }
}