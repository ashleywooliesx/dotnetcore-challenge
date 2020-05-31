using MartinCostello.Logging.XUnit;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using WooliesX.Challenge.Api;

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
            builder.ConfigureLogging(p => p.AddXUnit(this));
        }
    }
}