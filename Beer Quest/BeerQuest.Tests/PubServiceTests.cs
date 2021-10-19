using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using AutoFixture.Xunit2;

using Dawn;

using FluentAssertions;

using Moq;

using Newtonsoft.Json;

using Xunit;

namespace BeerQuest.Tests
{
    public class PubServiceTests
    {
        [Theory]
        [AutoData]
        public async Task GivenPubExists_WhenGettingPubByName_ThenReturnPub(string name)
        {
            // Arrange
            var mockedHttpClientService = new Mock<IHttpClientService>();
            var service = new PubService(mockedHttpClientService.Object);

            // Act
            var pub = await service.Get(name);

            // Assert
            pub.Should().NotBeNull($"A pub named '{name}' exists and is part of the Beer Quest.");
        }

        [Theory]
        [AutoData]
        public async Task GivenRequestUnsuccessful_WhenGettingPubByName_ThenReturnNull(string name)
        {
            // Arrange
            var pub = default(Pub);
            var mockedHttpClientService = new Mock<IHttpClientService>();
            mockedHttpClientService
                .Setup(service => service.Get<Pub>(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new HttpRequestException());

            var pubService = new PubService(mockedHttpClientService.Object);

            // Act
            async Task SutCall()
            {
                pub = await pubService.Get(name);
            }

            Func<Task> sutCall = SutCall;

            // Assert
            await sutCall.Should().NotThrowAsync<HttpRequestException>("No errors are expected to happen.");
            pub.Should().BeNull("A null object must be returned when an error occurs.");
        }

        [Fact]
        public async Task GivenNameIsNull_WhenGettingPubByName_ThenThrowArgumentNullException()
        {
            // Arrange
            var name = default(string);
            var mockedHttpClientService = new Mock<IHttpClientService>();
            var pubService = new PubService(mockedHttpClientService.Object);

            // Act
            async Task SutCall()
            {
                await pubService.Get(name);
            }

            Func<Task> sutCall = SutCall;

            // Assert
            await sutCall.Should()
                .ThrowAsync<ArgumentNullException>("Name cannot be null when querying pubs by name.");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public async Task GivenNameIsEmptyOrWhitespace_WhenGettingPubByName_ThenThrowArgumentException(string name)
        {
            // Arrange
            var mockedHttpClientService = new Mock<IHttpClientService>();
            var pubService = new PubService(mockedHttpClientService.Object);

            // Act
            async Task SutCall()
            {
                await pubService.Get(name);
            }

            Func<Task> sutCall = SutCall;

            // Assert
            await sutCall.Should()
                .ThrowAsync<ArgumentException>("Name cannot be empty or whitespace when querying pubs by name.");
        }
    }

    public interface IHttpClientService
    {
        Task<T> Get<T>(
            string uri,
            CancellationToken cancellationToken = default);
    }

    public class HttpClientService : IHttpClientService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public HttpClientService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = Guard.Argument(httpClientFactory, nameof(httpClientFactory)).NotNull().Value;
        }

        public async Task<T> Get<T>(
            string uri,
            CancellationToken cancellationToken = default)
        {
            using var client = this.httpClientFactory.CreateClient();
            var response = await client.GetAsync(uri, cancellationToken);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync(cancellationToken);

            return JsonConvert.DeserializeObject<T>(json);
        }
    }

    public class PubService
    {
        private const string Api = "https://datamillnorth.org/api/table/wk7xz_hf8bv";
        private readonly IHttpClientService httpClientService;

        public PubService(IHttpClientService httpClientService)
        {
            this.httpClientService = Guard.Argument(httpClientService, nameof(httpClientService)).NotNull().Value;
        }

        public async Task<Pub?> Get(
            string name,
            CancellationToken cancellationToken = default)
        {
            Guard.Argument(name, nameof(name)).NotNull();

            var uri = $"{Api}?name={name}";

            try
            {
                return await this.httpClientService.Get<Pub>(uri, cancellationToken);
            }
            catch (HttpRequestException e)
            {
                return default;
            }
        }
    }

    public class Pub
    {
        public string Name { get; set; }
    }
}
