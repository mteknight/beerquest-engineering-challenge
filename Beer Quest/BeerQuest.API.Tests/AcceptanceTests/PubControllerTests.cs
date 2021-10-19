using System.Threading.Tasks;

using BeerQuest.API.Controllers;
using BeerQuest.Data.Models;
using BeerQuest.Domain;
using BeerQuest.Domain.Mapper;
using BeerQuest.Domain.Services;
using BeerQuest.Domain.Tests;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

using Xunit;

namespace BeerQuest.API.Tests.AcceptanceTests
{
    public class PubControllerTests
    {
        private const string ExpectedPubJson = @"{
            'name': 'The White Rose',
            'category': 'Pub reviews',
            'url': 'http://leedsbeer.info/?p=96',
            'date': '2012-09-16T11:19:25',
            'excerpt': 'Standard mainline station pub. Nothing to love; nothing to hate. ',
            'thumbnail': 'http://leedsbeer.info/wp-content/uploads/2012/09/20120915_140254.jpg',
            'lat': 53.7949,
            'lng': -1.54737,
            'address': 'Leeds Railway Station \r\nLeeds LS1 4DY ',
            'phone': '',
            'twitter': '',
            'stars_beer': 1.5,
            'stars_atmosphere': 2.5,
            'stars_amenities': 2.5,
            'stars_value': 2.5,
            'tags': 'food,breakfast,free wifi,coffee,sofas'
        }";

        private readonly IServiceCollection services;

        public PubControllerTests()
        {
            this.services = DIHelper.GetServices();
        }

        [Fact]
        public async Task WhenPubExists_WhenQueryingPubsByName_ThenExpectMatchingData()
        {
            // Arrange
            const string pubName = "The White Rose";
            var pubMapper = this.services.GetConfiguredService<IPubMapper>();
            var expectedPubRow = JsonConvert.DeserializeObject<PubResponseRow>(ExpectedPubJson);
            var expectedPub = pubMapper.Map(expectedPubRow);
            var pubService = this.services.GetConfiguredService<IPubService>();
            var controller = new PubController(pubService);

            // Act
            var result = await controller.Get(pubName);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull($"A pub named '{pubName}' exists.");
            result.As<OkObjectResult>().Value.As<Pub>().Should().Be(expectedPub);
        }
    }
}
