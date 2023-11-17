using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace AzureFunction.ChatBot;

public class ConfigProvider : IConfigProvider
{
    public Dictionary<string, string> GetQnAs()
    {
        var json = File.ReadAllText("Config/appsettings.json");
        var config = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);
        return config?["QnAData"] ?? new Dictionary<string, string>();
    }
}
