using System;
using System.Threading.Tasks;

using FluentAssertions;

using Newtonsoft.Json;

using Xunit;
using Xunit.Abstractions;

namespace WooliesX.Challenge.IntegrationTests
{
    public class UserTests : IClassFixture<HttpServerFixture>, IDisposable
    {
        public UserTests(HttpServerFixture fixture, ITestOutputHelper outputHelper)
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
        public async Task Http_Get_Many()
        {
            // Arrange
            using var httpClient = Fixture.CreateClient();

            // Act
            using var response = await httpClient.GetAsync("api/user");

            // Assert
            response.EnsureSuccessStatusCode();

            var content = await response
                .Content
                .ReadAsStringAsync();

            var expected = new
            {
                Name = "Ashley Dass",
                Token = "2499dd7f-f06e-4073-8fae-fb28dbd9dc1e"
            };

            JsonConvert.DeserializeObject(content, expected.GetType())
                .Should()
                .BeEquivalentTo(expected);
        }
    }
}
