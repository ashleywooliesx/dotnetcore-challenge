using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using WooliesX.Challenge.Api.Controllers;
using WooliesX.Challenge.Api.Services;

namespace WooliesX.Challenge.Api.Queries
{
    public class GetSortedProductsQuery : IRequest<ProductsResponse>
    {
        public SortOption SortOption { get; }

        public GetSortedProductsQuery(SortOption sortOption)
        {
            SortOption = sortOption;
        }

        public class GetSortedProductsQueryHandler : IRequestHandler<GetSortedProductsQuery, ProductsResponse>
        {
            private readonly IProductsService _productsService;

            public GetSortedProductsQueryHandler(IProductsService productsService)
            {
                _productsService = productsService;
            }

            public async Task<ProductsResponse> Handle(GetSortedProductsQuery request, CancellationToken cancellationToken)
            {
                var products = await _productsService.GetProducts(cancellationToken);

                var sortedProducts = request.SortOption switch
                {
                    SortOption.Low => products.OrderBy(p => p.Price),
                    SortOption.High => products.OrderByDescending(p => p.Price),
                    SortOption.Ascending => products.OrderBy(p => p.Name),
                    SortOption.Descending => products.OrderByDescending(p => p.Name),
                    SortOption.Recommended => await GetProductsByRecommended(products, cancellationToken),
                    _ => throw new ArgumentOutOfRangeException(nameof(request.SortOption))
                };

                return new ProductsResponse(sortedProducts);
            }

            private async Task<IEnumerable<Product>> GetProductsByRecommended(
                IEnumerable<Product> products,
                CancellationToken cancellationToken)
            {
                var shoppingHistory = await _productsService.GetShoppingHistory(cancellationToken);
                var soldProducts = new Dictionary<string, SoldProduct>();

                foreach (var customerProducts in shoppingHistory)
                {
                    foreach (var customerProduct in customerProducts.Products)
                    {
                        if (soldProducts.ContainsKey(customerProduct.Name))
                        {
                            soldProducts[customerProduct.Name].QuantitySold += customerProduct.Quantity;
                            soldProducts[customerProduct.Name].SoldCount++;
                        }
                        else
                        {
                            soldProducts.Add(customerProduct.Name, new SoldProduct(customerProduct));
                        }
                    }
                }

                var sortedProducts = soldProducts.Values
                    .OrderByDescending(p => p.SoldCount)
                    .ThenByDescending(p => p.QuantitySold)
                    .Select(p => p.Product)
                    .ToList();

                sortedProducts
                    .AddRange(products.Except(sortedProducts, new ProductComparer()));

                return sortedProducts;
            }
        }
    }
}