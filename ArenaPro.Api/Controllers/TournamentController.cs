using ArenaPro.Application.Abstractions.TournamentServices;
using ArenaPro.Application.Models.TournamentModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArenaPro.Api.Controllers;
[Route("api/[controller]")]
[Authorize]
[ApiController]
public class TournamentController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromServices] ITournamentCreateServices services, [FromBody] TournamentModel model)
    {
        var created = await services.ExecuteAsync(model);
        if (!created) return BadRequest();
        return Created();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync([FromServices] ITournamentDeleteServices services, [FromQuery] int idTournament)
    {
        var deleted = await services.ExecuteAsync(idTournament);
        if (!deleted) return BadRequest();
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> PutAsync([FromServices] ITournamentUpdateServices services, [FromBody] TournamentUpdateModel model)
    {
        var updated = await services.ExecuteAsync(model);
        if (!updated) return BadRequest();
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromServices] ITournamentGetAllServices services)
    {
        var data = await services.ExecuteAsync();
        return Ok(data);
    }

    [HttpGet("byName")]
    public async Task<IActionResult> GetByNameAsync([FromServices] ITournamentGetByNameServices services, [FromQuery] string name)
    {
        var data = await services.ExecuteAsync(name);
        if (data == null) return NotFound();
        return Ok(data);
    }
}
