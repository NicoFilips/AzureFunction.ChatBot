using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AzureFunction.ChatBot;

public class SimpleQnABot
{
    private readonly IConfigProvider _configProvider;

    public SimpleQnABot(IConfigProvider configProvider)
    {
        _configProvider = configProvider;
    }
    
    [FunctionName("QnABot")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
    {
        string question = req.Query["question"];

        if (_configProvider.GetQnAs().TryGetValue(question, out string answer))
        {
            return new OkObjectResult(answer);
        }
        else
        {
            return new OkObjectResult("Question wasn't found.");
        }
    }
}
