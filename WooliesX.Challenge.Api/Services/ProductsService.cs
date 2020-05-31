using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Flurl;

using Microsoft.Extensions.Options;

using Newtonsoft.Json;

using WooliesX.Challenge.Api.Options;

namespace WooliesX.Challenge.Api.Services
{
    public interface IProductsService
    {
        Task<IEnumerable<Product>> GetProducts(CancellationToken cancellationToken);
    }

    internal class ProductsService : IProductsService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly UserOptions _userOptions;

        private readonly ResourceApiOptions _resourceApiOptions;

        public ProductsService(IHttpClientFactory httpClientFactory, IOptions<ResourceApiOptions> resourceApiOptions, IOptions<UserOptions> userOptions)
        {
            _httpClientFactory = httpClientFactory;
            _userOptions = userOptions.Value;
            _resourceApiOptions = resourceApiOptions.Value;
        }

        public async Task<IEnumerable<Product>> GetProducts(CancellationToken cancellationToken)
        {
            using var httpClient = _httpClientFactory.CreateClient();

            var resourceApiBaseUrl = Url.Combine(
                _resourceApiOptions.BaseUrl,
                $"products?token={_userOptions.Token}");

            var message = new HttpRequestMessage(HttpMethod.Get, resourceApiBaseUrl);

            var response = await httpClient.SendAsync(message, cancellationToken);

            return JsonConvert.DeserializeObject<IEnumerable<Product>>(await response.Content.ReadAsStringAsync());
        }
    }
}