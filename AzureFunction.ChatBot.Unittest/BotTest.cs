using AzureFunction.ChatBot;
using AzureFunction.ChatBot.Helper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

[TestFixture]
public class BotTest
{
    private Mock<HttpRequest> mockHttpRequest;
    private Dictionary<string, string> qnaData;

    private SimpleQnABot simpleQnABot;
    
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
        simpleQnABot = new SimpleQnABot(mockConfigProvider.Object);
    }

    [Test]
    public async Task Run_ShouldReturnCorrectAnswer_WhenQuestionExists()
    {
        // Arrange
        mockHttpRequest.Setup(req => req.Query).Returns(new QueryCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>
                                                                            {
                                                                                { "question", "TestQuestion" }
                                                                            }));
        // Act
        var result = await simpleQnABot.Run(mockHttpRequest.Object) as OkObjectResult;
        
        // Assert
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

        var result = await simpleQnABot.Run(mockHttpRequest.Object) as OkObjectResult;
        
        result.Should().NotBeNull();
        result.Value.Should().Be("Question wasn't found.");
    }
}
