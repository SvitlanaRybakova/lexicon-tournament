using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tournament.Data.Data;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using AutoMapper;
using Tournament.Core.DTOs.Tournament;
using Microsoft.AspNetCore.JsonPatch;

namespace Tournament.API.Controllers
{
    [Route("api/Tournaments")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly TournamentContext _context;

        // private readonly ITournamentRepository _tournamentRepository;

        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TournamentsController(TournamentContext context, ITournamentRepository tournamentRepository, IUnitOfWork uow, IMapper mapper)
        {
            _context = context;
            _uow = uow;
            _mapper = mapper;
            // _tournamentRepository = tournamentRepository;
        }

        // GET: api/Tournaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDto>>> GetTournament()
        {
            var tournaments = await _uow.TournamentRepository.GetAllAsync();
            var tournamentDto = _mapper.Map<IEnumerable<TournamentDto>>(tournaments);

            return Ok(tournamentDto);
        }

        // GET: api/Tournaments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentDto>> GetTournament(int id)
        {
            if (!await _uow.TournamentRepository.AnyAsync(id))
            {
                return NotFound();
            }

            var tournament = await _uow.TournamentRepository.GetAsync(id);
            var tournamentDto = _mapper.Map<TournamentDto>(tournament);

            return Ok(tournamentDto);
        }

        // PUT: api/Tournaments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournament(int id, CreateTournamentDto createTournamentDto)
        {
            if (!await _uow.TournamentRepository.AnyAsync(id)) return NotFound();


            if (!ModelState.IsValid) return BadRequest(ModelState);

            var tournament = await _uow.TournamentRepository.GetAsync(id);
            _mapper.Map(createTournamentDto, tournament);

            try
            {
                _uow.TournamentRepository.Update(tournament);
                await _uow.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "An error occurred while updating the tournament.");
                throw;
            }

            return NoContent();
        }

        // POST: api/Tournaments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TournamentModel>> PostTournament(CreateTournamentDto createTournamentDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var tournament = _mapper.Map<TournamentModel>(createTournamentDto);

            try
            {
                _uow.TournamentRepository.Add(tournament);
                await _uow.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "An error occurred while saving the tournament.");
                throw;
            }

            var tournamentDto = _mapper.Map<TournamentDto>(tournament);
            return CreatedAtAction(nameof(GetTournament), new { id = tournament.Id }, tournamentDto);
        }

        // DELETE: api/Tournaments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            var tournament = await _uow.TournamentRepository.GetAsync(id);
            if (tournament == null) return NotFound();
            try
            {
                _uow.TournamentRepository.Remove(tournament);
                await _uow.SaveAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "An error occurred while deleting the tournament.");
            }
        }

        // Patch api/Tournaments/5
        [HttpPatch("{id:int}")]
        public async Task<ActionResult> PatchTournament(int id, JsonPatchDocument<CreateTournamentDto> patchDocument)
        {
            if (patchDocument is null)
                return BadRequest("No patch document");

            if (!await _uow.TournamentRepository.AnyAsync(id))
                return NotFound("The tournament does not exist in the database");

            try
            {
                var tournamentModel = await _uow.TournamentRepository.GetAsync(id);
                var dto = _mapper.Map<CreateTournamentDto>(tournamentModel);
                patchDocument.ApplyTo(dto, ModelState);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                
                _mapper.Map(dto, tournamentModel);

                await _uow.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION: " + ex.ToString());
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}
