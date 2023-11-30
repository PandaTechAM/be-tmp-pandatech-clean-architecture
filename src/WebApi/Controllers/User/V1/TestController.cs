using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO.Response.Test;

namespace WebApi.Controllers.User.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[ApiExplorerSettings(GroupName = "userV1")]
public class TestController : ControllerBase
{
    /// <summary>
    /// Test admin endpoint
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet("user-endpoint")]
    public async Task<IActionResult> Get(string text)
    {
        await Task.Delay(100);
        return Ok(new TestResponse(){Text = text});
    }
}