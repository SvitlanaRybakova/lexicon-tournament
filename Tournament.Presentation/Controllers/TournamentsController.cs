using Microsoft.AspNetCore.Mvc;
using Tournament.Core.DTOs.Tournament;
using Microsoft.AspNetCore.JsonPatch;
using Tournament.Services;
using Tournament.Contracts;

namespace Tournament.Presentation.Controllers
{
    [Route("api/Tournaments")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly ITournamentService _tournamentService;

        public TournamentsController(ITournamentService tournamentService)
        {
            _tournamentService = tournamentService;
        }

        // GET: api/Tournaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDto>>> GetTournaments()
        {
            var tournaments = await _tournamentService.GetTournamentsAsync(includeGames: false);
            return Ok(tournaments);
        }

        // GET: api/Tournaments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentDto>> GetTournament(int id)
        {
            try
            {
                var tournament = await _tournamentService.GetTournamentAsync(id);
                return Ok(tournament);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Tournament not found.");
            }
        }

        // POST: api/Tournaments
        [HttpPost]
        public async Task<ActionResult> PostTournament([FromBody] TournamentDto tournamentDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _tournamentService.PostTournament(tournamentDto);

            // Return a CreatedAt route
            return CreatedAtAction(nameof(GetTournament), new { id = tournamentDto.Id }, tournamentDto);
        }

        // PUT: api/Tournaments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournament(int id, [FromBody] TournamentDto tournamentDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _tournamentService.PutTournament(id, tournamentDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Tournament not found.");
            }
        }

        // DELETE: api/Tournaments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            try
            {
                await _tournamentService.DeleteTournament(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Tournament not found.");
            }
        }

        // PATCH: api/Tournaments/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchTournament(int id, [FromBody] JsonPatchDocument<TournamentDto> patchDocument)
        {
            if (patchDocument == null) return BadRequest("Patch document cannot be null.");

            try
            {
               
               await _tournamentService.PatchTournament(id, patchDocument);

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Tournament not found.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
