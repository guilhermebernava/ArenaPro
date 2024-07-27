using ArenaPro.Application.Abstractions.MatchServices;
using ArenaPro.Application.Models;
using ArenaPro.Application.Models.MatchModels;
using ArenaPro.Application.Models.MatchValidations;
using Microsoft.AspNetCore.Mvc;

namespace ArenaPro.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MatchController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromServices] IMatchCreateServices services, [FromBody] MatchModel model)
    {
        var created = await services.ExecuteAsync(model);
        if (!created) return BadRequest();
        return Created();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync([FromServices] IMatchDeleteServices services, [FromQuery] int idMatch)
    {
        var deleted = await services.ExecuteAsync(idMatch);
        if (!deleted) return BadRequest();
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> PutAsync([FromServices] IMatchUpdateServices services, [FromBody] MatchUpdateModel model)
    {
        var updated = await services.ExecuteAsync(model);
        if (!updated) return BadRequest();
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromServices] IMatchGetAllServices services)
    {
        var data = await services.ExecuteAsync();
        return Ok(data);
    }

    [HttpGet]
    public async Task<IActionResult> GetByDateAsync([FromServices] IMatchGetByDateServices services, [FromQuery] MatchGetModel<DateTime> model)
    {
        var data = await services.ExecuteAsync(model);
        if (data == null) return NotFound();
        return Ok(data);
    }

    [HttpGet]
    public async Task<IActionResult> GetByTournamentIdAsync([FromServices] IMatchGetByTournamentIdServices services, [FromQuery] MatchGetModel<int> model)
    {
        var data = await services.ExecuteAsync(model);
        if (data == null) return NotFound();
        return Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> AddMatchResultAsync([FromServices] IMatchAddMatchResultServices services, [FromBody] List<MatchResultModel> model)
    {
        var created = await services.ExecuteAsync(model);
        if (!created) return BadRequest();
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> AddMatchKdaAsync([FromServices] IMatchAddPlayerKdaServices services, [FromBody] List<MatchPlayerKdaModel> model)
    {
        var created = await services.ExecuteAsync(model);
        if (!created) return BadRequest();
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveMatchResultAsync([FromServices] IMatchRemoveMatchResultServices services, [FromBody] List<MatchResultModel> model)
    {
        var created = await services.ExecuteAsync(model);
        if (!created) return BadRequest();
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveMatchKdaAsync([FromServices] IMatchRemovePlayerKdaServices services, [FromBody] List<MatchPlayerKdaModel> model)
    {
        var created = await services.ExecuteAsync(model);
        if (!created) return BadRequest();
        return Ok();
    }
}
