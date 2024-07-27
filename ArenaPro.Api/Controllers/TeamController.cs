using ArenaPro.Application.Abstractions.TeamServices;
using ArenaPro.Application.Models.TeamModels;
using Microsoft.AspNetCore.Mvc;

namespace ArenaPro.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TeamController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromServices] ITeamCreateServices services, [FromBody] TeamModel model)
    {
        var created = await services.ExecuteAsync(model);
        if (!created) return BadRequest();
        return Created();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync([FromServices] ITeamDeleteServices services, [FromQuery] int idTeam)
    {
        var deleted = await services.ExecuteAsync(idTeam);
        if (!deleted) return BadRequest();
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> PutAsync([FromServices] ITeamUpdateServices services, [FromBody] TeamUpdateModel model)
    {
        var updated = await services.ExecuteAsync(model);
        if (!updated) return BadRequest();
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromServices] ITeamGetAllServices services)
    {
        var data = await services.ExecuteAsync();
        return Ok(data);
    }

    [HttpGet]
    public async Task<IActionResult> GetByNameAsync([FromServices] ITeamGetByNameServices services, [FromQuery] string name)
    {
        var data = await services.ExecuteAsync(name);
        if (data == null) return NotFound();
        return Ok(data);
    }
}
