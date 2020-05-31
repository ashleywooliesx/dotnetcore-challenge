using Microsoft.AspNetCore.Mvc;

namespace WooliesX.Challenge.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(
                new
                {
                    Name = "Ashley Dass",
                    Token = "2499dd7f-f06e-4073-8fae-fb28dbd9dc1e"
                });
        }
    }
}
