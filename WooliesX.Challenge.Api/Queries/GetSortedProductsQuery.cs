using System;
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
                    _ => throw new ArgumentOutOfRangeException(nameof(request.SortOption))
                };

                return new ProductsResponse(sortedProducts);
            }
        }
    }
}