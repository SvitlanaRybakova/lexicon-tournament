using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Contracts;
using Tournament.Core.DTOs.Game;
using Microsoft.AspNetCore.JsonPatch;

namespace Tournament.Services
{
    public class GameSevice : IGameService
    {
        public Task DeleteGame(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GameDto> GetGameAsync(string identifier, bool trackChanges = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GameDto>> GetGamesAsync(bool trackChanges = false)
        {
            throw new NotImplementedException();
        }

        public Task PatchGame(int id, Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<GameDto> patchDocument)
        {
            throw new NotImplementedException();
        }

        public Task PostGame(CreateGameDto dto)
        {
            throw new NotImplementedException();
        }

        public Task PutTournament(int id, GameDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
