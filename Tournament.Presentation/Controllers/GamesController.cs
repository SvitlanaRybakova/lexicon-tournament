using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Tournament.Contracts;
using Tournament.Core.DTOs.Game;

[Route("api/Games")]
[ApiController]
public class GamesController : ControllerBase
{
    private readonly IGameService _gameService;

    public GamesController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GameDto>>> GetGames()
    {
        var games = await _gameService.GetGamesAsync();
        return Ok(games);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GameDto>> GetGame(int id)
    {
        try
        {
            var game = await _gameService.GetGameAsync(id.ToString());
            return Ok(game);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Game not found.");
        }
    }

    [HttpPost]
    public async Task<ActionResult> PostGame([FromBody] GameDto createGameDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _gameService.PostGame(createGameDto);
        return CreatedAtAction(nameof(GetGame), new { id = createGameDto.Id }, createGameDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutGame(int id, [FromBody] GameDto gameDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            await _gameService.PutGame(id, gameDto);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Game not found.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGame(int id)
    {
        try
        {
            await _gameService.DeleteGame(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Game not found.");
        }
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchGame(int id, [FromBody] JsonPatchDocument<GameDto> patchDocument)
    {
        if (patchDocument == null) return BadRequest("Patch document cannot be null.");

        try
        {
            await _gameService.PatchGame(id, patchDocument);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Game not found.");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
