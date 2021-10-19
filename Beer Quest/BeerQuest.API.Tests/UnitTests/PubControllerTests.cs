using System.Threading;
using System.Threading.Tasks;

using AutoFixture.Xunit2;

using BeerQuest.API.Controllers;
using BeerQuest.Domain;
using BeerQuest.Domain.Services;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace BeerQuest.API.Tests.UnitTests
{
    public class PubControllerTests
    {
        [Theory]
        [AutoData]
        public async Task GivenPubExists_WhenQueryingPubsByName_ThenReturnPub(Pub pub)
        {
            // Arrange
            var mockedPubService = new Mock<IPubService>();
            mockedPubService
                .Setup(service => service.Get(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(pub);

            var controller = new PubController(mockedPubService.Object);

            // Act
            var result = await controller.Get(pub.Name);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.As<Pub>().Name.Should().NotBeNull("The pub instance should be returned in the response.");
        }

        [Theory]
        [InlineData(default(string))]
        [InlineData("")]
        [InlineData(" ")]
        public async Task GivenInvalidName_WhenQueryingPubsByName_ThenExpectBadRequest(string name)
        {
            // Arrange
            var mockedPubService = new Mock<IPubService>();
            var controller = new PubController(mockedPubService.Object);

            // Act
            var result = await controller.Get(name);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
