using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tournament.Data.Data;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;

namespace Tournament.API.Controllers
{
    [Route("api/Tournaments")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly TournamentContext _context;
        private readonly ITournamentRepository _tournamentRepository;

        public TournamentsController(TournamentContext context, ITournamentRepository tournamentRepository)
        {
            _context = context;
            _tournamentRepository = tournamentRepository;
        }

        // GET: api/Tournaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentModel>>> GetTournament()
        {
            var tournaments = await _tournamentRepository.GetAllAsync();
            return Ok(tournaments);
        }

        // GET: api/Tournaments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentModel>> GetTournament(int id)
        {
            if (!await _tournamentRepository.AnyAsync(id))
            {
                return NotFound();
            }

            var tournament = await _tournamentRepository.GetAsync(id);
            return Ok(tournament);
        }

        // PUT: api/Tournaments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournament(int id, TournamentModel tournament)
        {
            if (id != tournament.Id)
            {
                return BadRequest();
            }

            if (!await _tournamentRepository.AnyAsync(id))
            {
                return NotFound();
            }

            try
            {
                _tournamentRepository.Update(tournament);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/Tournaments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TournamentModel>> PostTournament(TournamentModel tournament)
        {
            if (await _tournamentRepository.AnyAsync(tournament.Id))
            {
                return Conflict("A tournament with this ID already exists.");
            }

            _tournamentRepository.Add(tournament);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTournament", new { id = tournament.Id }, tournament);
        }

        // DELETE: api/Tournaments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            var tournament = await _tournamentRepository.GetAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }

            _tournamentRepository.Remove(tournament);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
