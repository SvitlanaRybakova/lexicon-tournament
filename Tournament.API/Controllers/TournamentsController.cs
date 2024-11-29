using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tournament.Data.Data;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using AutoMapper;
using Tournament.Core.DTOs.Tournament;

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
            if (!await _uow.TournamentRepository.AnyAsync(id))
            {
                return NotFound();
            }

            var tournament = await _uow.TournamentRepository.GetAsync(id);
            _mapper.Map(createTournamentDto, tournament);

            try
            {
                _uow.TournamentRepository.Update(tournament);
                await _uow.SaveAsync();
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
        public async Task<ActionResult<TournamentModel>> PostTournament(CreateTournamentDto createTournamentDto)
        {
            var tournament = _mapper.Map<TournamentModel>(createTournamentDto);
            _uow.TournamentRepository.Add(tournament);
            await _uow.SaveAsync();

            var tournamentDto = _mapper.Map<TournamentDto>(tournament);
            return CreatedAtAction(nameof(GetTournament), new { id = tournament.Id }, tournamentDto);
        }

        // DELETE: api/Tournaments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            var tournament = await _uow.TournamentRepository.GetAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }

            _uow.TournamentRepository.Remove(tournament);
            await _uow.SaveAsync();

            return NoContent();
        }


    }
}
