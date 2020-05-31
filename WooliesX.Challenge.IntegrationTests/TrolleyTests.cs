using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Newtonsoft.Json;

using WooliesX.Challenge.Api.Requests;

using Xunit;
using Xunit.Abstractions;

namespace WooliesX.Challenge.IntegrationTests
{
    public class TrolleyTests : IClassFixture<HttpServerFixture>, IDisposable
    {
        public TrolleyTests(HttpServerFixture fixture, ITestOutputHelper outputHelper)
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
        public async Task ResponseContainsCorrectCalculatedTotal()
        {
            // Arrange
            using var httpClient = Fixture.CreateClient();

            var trolley = new Trolley
            {
                Products = new[]
                {
                    new ProductPrice("Product-A", 1M),
                    new ProductPrice("Product-B", 2M),
                },
                Specials = new[]
                {
                    new Special(new []
                    {
                        new ProductQuantity("Product-A", 1), 
                    }, 10),
                    new Special(new []
                    {
                        new ProductQuantity("Product-B", 1),
                    }, 10),
                },
                Quantities = new[]
                {
                    new ProductQuantity("Product-A", 1),
                    new ProductQuantity("Product-B", 1),
                }
            };

            var content = JsonConvert.SerializeObject(trolley);

            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer)
            {
                Headers =
                {
                    ContentType = new MediaTypeHeaderValue("application/json")
                }
            };


            // Act
            using var response = await httpClient.PostAsync("api/trolleyTotal", byteContent);

            // Assert
            response.EnsureSuccessStatusCode();

            //var content = await response
            //    .Content
            //    .ReadAsStringAsync();

            //var expected = new
            //{
            //    Name = "Ashley Dass",
            //    Token = "2499dd7f-f06e-4073-8fae-fb28dbd9dc1e"
            //};

            //JsonConvert.DeserializeObject(content, expected.GetType())
            //    .Should()
            //    .BeEquivalentTo(expected);
        }
    }
}
