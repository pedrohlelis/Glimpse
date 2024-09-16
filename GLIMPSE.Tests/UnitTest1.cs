using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace GLIMPSE.Tests
{
    public class CardIntegrationTests : IDisposable
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;
        public CardIntegrationTests()
        {
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();
        }
        [SetUp]
        public void Setup()
        {

        }
        [Test]
        public async Task GetCardInfo_ShouldReturnNotFound_ForInvalidCardId()
        {
            var response = await _client.GetAsync("/Card/GetCardInfo/999");

            Assert.AreEqual(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        public void Dispose()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}
