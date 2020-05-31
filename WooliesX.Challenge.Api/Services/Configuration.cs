using Microsoft.Extensions.DependencyInjection;

namespace WooliesX.Challenge.Api.Services
{
    public static class Configuration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddTransient<IProductsService, ProductsService>();
        }
    }
}