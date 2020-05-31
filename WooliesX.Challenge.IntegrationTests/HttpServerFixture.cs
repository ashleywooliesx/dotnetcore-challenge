using MartinCostello.Logging.XUnit;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using WooliesX.Challenge.Api;
using WooliesX.Challenge.Api.Options;

using Xunit.Abstractions;

namespace WooliesX.Challenge.IntegrationTests
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class HttpServerFixture : WebApplicationFactory<Program>, ITestOutputHelperAccessor
    {
        public ITestOutputHelper OutputHelper { get; set; }

        protected override IHostBuilder CreateHostBuilder()
        {
            var builder = base.CreateHostBuilder();
            builder.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddXUnit(OutputHelper);
            });

            return builder;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(
                services =>
                {
                    services.Configure<UserOptions>(
                        options =>
                        {
                            options.Name = "Ashley Dass";
                            options.Token = "2499dd7f-f06e-4073-8fae-fb28dbd9dc1e";
                        });
                    services.Configure<ResourceApiOptions>(
                        options =>
                        {
                            options.BaseUrl = "http://dev-wooliesx-recruitment.azurewebsites.net/api/resource";
                        });
                });

            builder.ConfigureLogging(p => p.AddXUnit(this));
        }
    }
}