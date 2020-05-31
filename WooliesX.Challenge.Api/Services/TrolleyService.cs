using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

using Flurl;

using Microsoft.Extensions.Options;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using WooliesX.Challenge.Api.Options;
using WooliesX.Challenge.Api.Requests;

namespace WooliesX.Challenge.Api.Services
{
    public interface ITrolleyService
    {
        Task<decimal> CalculateTotal(
            IEnumerable<ProductPrice> products,
            IEnumerable<Special> specials,
            IEnumerable<ProductQuantity> quantities,
            CancellationToken cancellationToken);
    }

    public class TrolleyService : ITrolleyService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly ResourceApiOptions _resourceApiOptions;

        private readonly UserOptions _userOptions;

        public TrolleyService(IHttpClientFactory httpClientFactory, IOptions<ResourceApiOptions> resourceApiOptions, IOptions<UserOptions> userOptions)
        {
            _httpClientFactory = httpClientFactory;
            _userOptions = userOptions.Value;
            _resourceApiOptions = resourceApiOptions.Value;
        }

        public async Task<decimal> CalculateTotal(
            IEnumerable<ProductPrice> products,
            IEnumerable<Special> specials,
            IEnumerable<ProductQuantity> quantities,
            CancellationToken cancellationToken)
        {
            using var httpClient = _httpClientFactory.CreateClient();

            var resourceApiBaseUrl = Url.Combine(
                _resourceApiOptions.BaseUrl,
                $"trolleyCalculator?token={_userOptions.Token}");

            var content = JsonConvert.SerializeObject(
                new
                {
                    products, specials, quantities
                },
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer)
            {
                Headers =
                {
                    ContentType = new MediaTypeHeaderValue("application/json-patch+json")
                }
            };
            
            var response = await httpClient.PostAsync(resourceApiBaseUrl, byteContent, cancellationToken);
            var responseContent = await response.Content.ReadAsStringAsync();

            return response.IsSuccessStatusCode
                ? decimal.Parse(responseContent)
                : throw new Exception($"The resource API returned a failure: {responseContent}");
        }
    }
}