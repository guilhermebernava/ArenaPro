using ArenaPro.Application.Abstractions.PlayerServices;
using ArenaPro.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArenaPro.Api.Controllers;
[Route("api/[controller]")]
[Authorize]
[ApiController]
public class PlayerController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromServices] IPlayerCreateServices services, [FromBody] PlayerModel model)
    {
        var created = await services.ExecuteAsync(model);
        if (!created) return BadRequest();
        return Created();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync([FromServices] IPlayerDeleteServices services, [FromQuery] int idPlayer)
    {
        var deleted = await services.ExecuteAsync(idPlayer);
        if (!deleted) return BadRequest();
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> PutAsync([FromServices] IPlayerUpdateServices services, [FromBody] PlayerModel model)
    {
        var updated = await services.ExecuteAsync(model);
        if (!updated) return BadRequest();
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromServices] IPlayerGetAllServices services)
    {
        var data = await services.ExecuteAsync();
        return Ok(data);
    }

    [HttpGet("byNick")]
    public async Task<IActionResult> GetByNickAsync([FromServices] IPlayerGetByNickServices services, [FromQuery] string nick)
    {
        var data = await services.ExecuteAsync(nick);
        if(data == null) return NotFound();
        return Ok(data);
    }

    [HttpGet("byTeamId")]
    public async Task<IActionResult> GetByTeamIdAsync([FromServices] IPlayerGetByTeamIdServices services, [FromQuery] int teamId)
    {
        var data = await services.ExecuteAsync(teamId);
        if (data == null) return NotFound();
        return Ok(data);
    }
}
