using System.Threading.Tasks;

using BeerQuest.API.Controllers;
using BeerQuest.Domain;
using BeerQuest.Domain.Services;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Xunit;

namespace BeerQuest.API.Tests.AcceptanceTests
{
    public class PubControllerTests
    {
        [Fact]
        public async Task WhenPubExists_WhenQueryingPubsByName_ThenExpectMatchingData()
        {
            // Arrange
            var expectedPub = new Pub {Name = "The White Rose"};
            var pubService = DIHelper.GetServices().GetConfiguredService<IPubService>();
            var controller = new PubController(pubService);

            // Act
            var result = await controller.Get(expectedPub.Name);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull($"A pub named '{expectedPub.Name}' exists.");
            result.As<OkObjectResult>().Value.As<Pub>().Should().Be(expectedPub);
        }
    }
}
