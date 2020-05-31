using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using FluentAssertions;

using Newtonsoft.Json;

using WooliesX.Challenge.Api.Services;

using Xunit;
using Xunit.Abstractions;

namespace WooliesX.Challenge.IntegrationTests
{
    public class SortTests : IClassFixture<HttpServerFixture>, IDisposable
    {
        public SortTests(HttpServerFixture fixture, ITestOutputHelper outputHelper)
        {
            Fixture = fixture;
            Fixture.OutputHelper = outputHelper;
        }

        private HttpServerFixture Fixture { get; }

        public void Dispose()
        {
            Fixture.OutputHelper = null;
        }

        [Fact]
        public async Task ResponseContainsProducts()
        {
            // Arrange
            using var httpClient = Fixture.CreateClient();

            // Act
            using var response = await httpClient.GetAsync("api/sort?sortOption=Recommended");

            // Assert
            response
                .EnsureSuccessStatusCode();

            var content =
                JsonConvert.DeserializeObject<IEnumerable<Product>>(await response.Content.ReadAsStringAsync());

            content
                .Should()
                .NotContainNulls();
        }
    }
}