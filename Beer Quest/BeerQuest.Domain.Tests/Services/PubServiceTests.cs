using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using AutoFixture.Xunit2;

using BeerQuest.Data.Services;
using BeerQuest.Domain.Services;

using FluentAssertions;

using Moq;

using Xunit;

namespace BeerQuest.Domain.Tests.Services
{
    public class PubServiceTests
    {
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
}
