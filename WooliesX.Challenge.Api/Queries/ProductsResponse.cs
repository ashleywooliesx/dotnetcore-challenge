using System.Collections.Generic;

using WooliesX.Challenge.Api.Services;

namespace WooliesX.Challenge.Api.Queries
{
    public class ProductsResponse
    {
        public ProductsResponse(IEnumerable<Product> sortedProducts)
        {
            Products = sortedProducts;
        }

        public IEnumerable<Product> Products { get; }
    }
}