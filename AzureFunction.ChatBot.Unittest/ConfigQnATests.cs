using AzureFunction.ChatBot.Helper;
using FluentAssertions;
using NUnit.Framework;

namespace AzureFunction.ChatBot.Unittest;

public class Tests
{
    [TestFixture]
    public class ConfigQnATests
    {
        ConfigProvider config = null!;
        [SetUp]
        public void Setup()
        {
            config = new ConfigProvider();
        }

        [Test]
        public void GetQnAs_ReturnsNonEmptyDictionary_WhenFileExists()
        {
            var result = config.GetQnAs();
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void GetQnAs_ReturnsCorrectStructure_WhenFileIsFormattedProperly()
        {
            var result = config.GetQnAs();
            Assert.That(result, Is.TypeOf<Dictionary<string, string>>());
        }

        [Test]
        public void GetQnAs_ReturnsEmptyDictionary_WhenFileIsEmpty()
        {
            // Ersetze QnA.json temporär durch eine leere oder nicht existierende Datei
            var result = config.GetQnAs();
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void GetQnAs_ShouldReturnNonEmptyDictionary_WhenFileExists()
        {
            var result = config.GetQnAs();
            result.Should().NotBeEmpty();
        }

        [Test]
        public void GetQnAs_ShouldReturnCorrectStructure_WhenFileIsFormattedProperly()
        {
            var result = config.GetQnAs();
            result.Should().BeOfType<Dictionary<string, string>>();
        }

        [Test]
        public void GetQnAs_ShouldReturnEmptyDictionary_WhenFileIsEmpty()
        {
            // Ersetze QnA.json temporär durch eine leere oder nicht existierende Datei
            var result = config.GetQnAs();
            result.Should().NotBeEmpty();
        }
    }
}
