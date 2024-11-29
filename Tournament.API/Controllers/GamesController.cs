using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tournament.Data.Data;
using Tournament.Core.Entities;
using Tournament.Core.DTOs.Game;
using AutoMapper;
using Tournament.Core.Repositories;

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
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGame()
        {
            var games = await _uow.GameRepository.GetAllAsync();
            var gamesDto = _mapper.Map<IEnumerable<GameDto>>(games);

            return Ok(gamesDto);
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> GetGame(int id)
        {
            if (!await _uow.GameRepository.AnyAsync(id))
            {
                return NotFound();
            }

            var game = await _uow.GameRepository.GetAsync(id);
            var gameDto = _mapper.Map<GameDto>(game);

            return Ok(gameDto);
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, CreateGameDto createGameDto)
        {
            if (!await _uow.GameRepository.AnyAsync(id)) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);



            var game = await _uow.GameRepository.GetAsync(id);
            _mapper.Map(createGameDto, game);

            try
            {
                _uow.GameRepository.Update(game);
                await _uow.SaveAsync();
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
        public async Task<ActionResult<GameDto>> PostGame(CreateGameDto createGameDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
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

            try
            {
                _uow.GameRepository.Add(game);
                await _uow.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "An error occurred while saving the tournament.");
                throw;
            }


            var gameDto = _mapper.Map<GameDto>(game);

            return CreatedAtAction(nameof(GetGame), new { id = game.Id }, gameDto);
        }


        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await _uow.GameRepository.GetAsync(id);
            if (game == null) return NotFound();
            try
            {
                _uow.GameRepository.Remove(game);
                await _uow.SaveAsync();

                return NoContent();

            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "An error occurred while deleting the game.");
            }


        }
    }
}
