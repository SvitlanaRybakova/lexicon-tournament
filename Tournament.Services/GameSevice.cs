using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tournament.Contracts;
using Tournament.Core.DTOs.Game;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;

namespace Tournament.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GameService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task DeleteGame(int id)
        {
            var game = await _uow.GameRepository.GetAsync(id);
            if (game == null)
            {
                throw new KeyNotFoundException("Game not found.");
            }

            _uow.GameRepository.Remove(game);
            await _uow.SaveAsync();
        }

        public async Task<GameDto> GetGameAsync(string identifier, bool trackChanges = false)
        {
            int id = int.TryParse(identifier, out var parsedId) ? parsedId : 0;
            var game = await _uow.GameRepository.GetAsync(id);
            if (game == null)
            {
                throw new KeyNotFoundException("Game not found.");
            }

            return _mapper.Map<GameDto>(game);
        }

        public async Task<IEnumerable<GameDto>> GetGamesAsync(bool trackChanges = false)
        {
            var games = await _uow.GameRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GameDto>>(games);
        }

        public async Task PatchGame(int id, JsonPatchDocument<GameDto> patchDocument)
        {
            if (patchDocument == null)
            {
                throw new ArgumentNullException(nameof(patchDocument), "Patch document cannot be null.");
            }

            var game = await _uow.GameRepository.GetAsync(id);
            if (game == null)
            {
                throw new KeyNotFoundException("Game not found.");
            }

            var gameDto = _mapper.Map<GameDto>(game);
            patchDocument.ApplyTo(gameDto);

            if (!ValidateModelState(gameDto))
            {
                throw new InvalidOperationException("Invalid patch document.");
            }

            _mapper.Map(gameDto, game);
            await _uow.SaveAsync();
        }

        public async Task PostGame(GameDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Game data cannot be null.");
            }

            var game = _mapper.Map<Game>(dto);
            if (await _uow.GameRepository.AnyAsync(game.Id))
            {
                throw new InvalidOperationException("A game with this ID already exists.");
            }

            _uow.GameRepository.Add(game);
            await _uow.SaveAsync();
        }

        public async Task PutGame(int id, GameDto dto)
        {
            var game = await _uow.GameRepository.GetAsync(id);
            if (game == null)
            {
                throw new KeyNotFoundException("Game not found.");
            }

            _mapper.Map(dto, game);
            _uow.GameRepository.Update(game);
            await _uow.SaveAsync();
        }

        private bool ValidateModelState<T>(T dto)
        {
            // todo
            return true;
        }
    }
}
