using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tournament.Data.Data;
using Tournament.Core.Entities;
using Tournament.Core.DTOs.Game;
using AutoMapper;
using Tournament.Core.Repositories;
using Tournament.Data.Repositories;

namespace Tournament.API.Controllers
{
    [Route("api/Games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly TournamentContext _context;
        private readonly IMapper _mapper;
        // private readonly IGameRepository _gameRepository;
        private readonly IUnitOfWork _uow;

        public GamesController(TournamentContext context, IMapper mapper, IGameRepository gameRepository, IUnitOfWork uow)
        {
            _context = context;
            _mapper = mapper;
            _uow = uow;
            // _gameRepository = gameRepository;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGame()
        {
            var games = await _uow.GameRepository.GetAllAsync();
            return Ok(games);
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            if (!await _uow.GameRepository.AnyAsync(id))
            {
                return NotFound();
            }

            var game = await _uow.GameRepository.GetAsync(id);
            return Ok(game);
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, Game game)
        {
            if (id != game.Id)
            {
                return BadRequest();
            }

            if (!await _uow.GameRepository.AnyAsync(id))
            {
                return NotFound();
            }

            try
            {
                _uow.GameRepository.Update(game);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(CreateGameDto createGameDto)
        {
           
            //var game = new Game
            //{
            //    Title = createGameDto.Title,
            //    Time = createGameDto.Time,
            //    Description = createGameDto.Description,
            //    TournamentModelId = createGameDto.TournamentModelId
            //};

            var game = _mapper.Map<Game>(createGameDto);

            if (await _uow.GameRepository.AnyAsync(game.Id))
            {
                return Conflict("A tournament with this ID already exists.");
            }

            _uow.GameRepository.Add(game);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGame", new { id = game.Id }, game);
        }


        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await _uow.GameRepository.GetAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _uow.GameRepository.Remove(game);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
