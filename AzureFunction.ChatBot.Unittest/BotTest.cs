using AzureFunction.ChatBot;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using YourNamespace; // Ersetze dies durch den tatsächlichen Namespace deiner Klasse

[TestFixture]
public class BotTest
{
    private Mock<HttpRequest> mockHttpRequest;
    private Dictionary<string, string> qnaData;

    [SetUp]
    public void Setup()
    {
        mockHttpRequest = new Mock<HttpRequest>();
        qnaData = new Dictionary<string, string>
                  {
                      { "TestQuestion", "TestAnswer" }
                      // Füge weitere Testdaten hinzu
                  };

        // Mock ConfigProvider.GetQnAs() - Ersetze ConfigProvider mit dem tatsächlichen Namen
        Mock<IConfigProvider> mockConfigProvider = new Mock<IConfigProvider>();
        mockConfigProvider.Setup(config => config.GetQnAs()).Returns(qnaData);
    }

    [Test]
    public async Task Run_ShouldReturnCorrectAnswer_WhenQuestionExists()
    {
        mockHttpRequest.Setup(req => req.Query).Returns(new QueryCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>
                                                                            {
                                                                                { "question", "TestQuestion" }
                                                                            }));

        var result = await SimpleQnABot.Run(mockHttpRequest.Object) as OkObjectResult;
        
        result.Should().NotBeNull();
        result.Value.Should().Be("TestAnswer");
    }

    [Test]
    public async Task Run_ShouldReturnNotFoundMessage_WhenQuestionDoesNotExist()
    {
        mockHttpRequest.Setup(req => req.Query).Returns(new QueryCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>
                                                                            {
                                                                                { "question", "NonExistentQuestion" }
                                                                            }));

        var result = await SimpleQnABot.Run(mockHttpRequest.Object) as OkObjectResult;
        
        result.Should().NotBeNull();
        result.Value.Should().Be("Question wasn't found.");
    }
}
