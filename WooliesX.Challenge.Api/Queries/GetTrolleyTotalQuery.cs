using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using WooliesX.Challenge.Api.Requests;
using WooliesX.Challenge.Api.Services;

namespace WooliesX.Challenge.Api.Queries
{
    public class GetTrolleyTotalQuery : IRequest<decimal>
    {
        public IEnumerable<ProductPrice> ProductPrices { get; }

        public IEnumerable<Special> Specials { get; }

        public IEnumerable<ProductQuantity> ProductQuantities { get; }

        public GetTrolleyTotalQuery(
            IEnumerable<ProductPrice> productPrices,
            IEnumerable<Special> specials,
            IEnumerable<ProductQuantity> productQuantities)
        {
            ProductPrices = productPrices;
            Specials = specials;
            ProductQuantities = productQuantities;
        }

        public class GetTrolleyTotalQueryHandler : IRequestHandler<GetTrolleyTotalQuery, decimal>
        {
            private readonly ITrolleyService _trolleyService;

            public GetTrolleyTotalQueryHandler(ITrolleyService trolleyService)
            {
                _trolleyService = trolleyService;
            }

            public Task<decimal> Handle(GetTrolleyTotalQuery request, CancellationToken cancellationToken)
            {
                var total = _trolleyService.CalculateTotal(
                    request.ProductPrices,
                    request.Specials,
                    request.ProductQuantities,
                    cancellationToken);

                return total;
            }
        }
    }
}