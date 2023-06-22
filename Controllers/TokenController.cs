using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly Tokenizer _tokenizer;

        public TokenController(Tokenizer tokenizer)
        {
            _tokenizer = tokenizer;
        }

        [HttpPost("generate")]
        public IActionResult GenerateToken([FromForm] string appKey, int userId)
        {
            // Call the GenerateToken method of the Tokenizer class
            string token = _tokenizer.GenerateToken(appKey, userId);

            // Return the generated token as a response
            return Ok(new { Token = token });
        }
    }

}
