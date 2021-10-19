using System;

using AutoFixture.Xunit2;

using FluentAssertions;

using Xunit;

namespace BeerQuest.Tests
{
    public class PubServiceTests
    {
        [Theory]
        [AutoData]
        public void GivenPubExists_WhenGettingPubByName_ExpectPubRetrieved(string name)
        {
            // Arrange
            var service = new PubService();

            // Act
            var pub = service.Get(name);

            // Assert
            pub.Should().NotBeNull($"A pub named '{name}' exists and is part of the Beer Quest.");
        }
    }

    public class PubService
    {
        public Pub Get(string name)
        {
            throw new NotImplementedException();
        }
    }

    public class Pub
    {
        public string Name { get; set; }
    }
}
