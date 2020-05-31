using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using WooliesX.Challenge.Api.Options;

namespace WooliesX.Challenge.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserOptions _userOptions;

        public UserController(IOptions<UserOptions> userOptions)
        {
            _userOptions = userOptions.Value;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(
                new
                {
                    _userOptions.Name,
                    _userOptions.Token
                });
        }
    }
}
