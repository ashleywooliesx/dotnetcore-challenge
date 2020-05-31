using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using WooliesX.Challenge.Api.Queries;
using WooliesX.Challenge.Api.Requests;

namespace WooliesX.Challenge.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrolleyTotalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TrolleyTotalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Trolley trolley)
        {
            var total = await _mediator.Send(new GetTrolleyTotalQuery(trolley.Products, trolley.Specials, trolley.Quantities));

            return Ok(total);
        }
    }
}
