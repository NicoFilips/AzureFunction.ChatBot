using System.Collections.Generic;

namespace AzureFunction.ChatBot;

public interface IConfigProvider
{
    public Dictionary<string, string> GetQnAs();
}
