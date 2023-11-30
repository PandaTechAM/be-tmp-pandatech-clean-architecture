using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Admin;

[ApiController]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[ApiExplorerSettings(GroupName = "admin")]
public class AdminController : ControllerBase
{
    /// <summary>
    /// Test admin endpoint
    /// </summary>
    /// <param name="useCase"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet("admin-endpoint")]
    public async Task<IActionResult> Get(string useCase)
    {
        await Task.Delay(100);
        return Ok(useCase);
    }
}