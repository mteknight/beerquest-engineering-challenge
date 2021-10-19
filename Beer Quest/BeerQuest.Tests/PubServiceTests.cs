using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using AutoFixture.Xunit2;

using Dawn;

using FluentAssertions;

using Newtonsoft.Json;

using Xunit;

namespace BeerQuest.Tests
{
    public class PubServiceTests
    {
        [Theory]
        [AutoData]
        public async Task GivenPubExists_WhenGettingPubByName_ExpectPubRetrieved(string name)
        {
            // Arrange
            var service = new PubService();

            // Act
            var pub = await service.Get(name);

            // Assert
            pub.Should().NotBeNull($"A pub named '{name}' exists and is part of the Beer Quest.");
        }

        [Theory]
        [AutoData]
        public void GivenRequestUnsuccessful_WhenMakingHttpGetRequest_ExpectHttpRequestException(string uri)
        {
            // Arrange
            var apiClient = new HttpClientService();

            // Act
            async Task SutCall()
            {
                await apiClient.Get<Pub>(uri);
            }

            Action sutCall = async () => await SutCall();

            // Assert
            sutCall.Should().ThrowExactly<HttpRequestException>("Http call must ensure a successful result.");
        }
    }

    public class HttpClientService
    {
        public Task<T> Get<T>(string uri)
        {
            throw new NotImplementedException();
        }
    }

    public class PubService
    {
        private const string api = "https://datamillnorth.org/api/table/wk7xz_hf8bv";
        private readonly IHttpClientFactory httpClientFactory;

        public PubService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = Guard.Argument(httpClientFactory, nameof(httpClientFactory)).NotNull().Value;
        }

        public async Task<Pub> Get(
            string name,
            CancellationToken cancellationToken = default)
        {
            var uri = $"{api}?name={name}";
            using var client = this.httpClientFactory.CreateClient();
            var response = await client.GetAsync(uri, cancellationToken);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync(cancellationToken);

            return JsonConvert.DeserializeObject<Pub>(json);
        }
    }

    public class Pub
    {
        public string Name { get; set; }
    }
}
