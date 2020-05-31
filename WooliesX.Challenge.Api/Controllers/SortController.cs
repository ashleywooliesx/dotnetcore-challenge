using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using WooliesX.Challenge.Api.Queries;

namespace WooliesX.Challenge.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SortController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SortController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Sort([FromQuery] SortOption sortOption)
        {
            var response = await _mediator.Send(new GetSortedProductsQuery(sortOption));

            return Ok(response.Products);
        }
    }

    public enum SortOption
    {
        Low,
        High,
        Ascending,
        Descending,
        Recommended
    }
}
