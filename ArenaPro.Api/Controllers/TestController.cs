using ArenaPro.Application.Abstractions.PlayerServices;
using ArenaPro.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace ArenaPro.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromServices] IPlayerCreateServices services, [FromBody] PlayerModel model)
    {
        var created = await services.ExecuteAsync(model);
        if (created) return Created();
        return BadRequest();
    }
}
