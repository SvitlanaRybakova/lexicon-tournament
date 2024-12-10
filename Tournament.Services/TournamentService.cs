using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Contracts;
using Tournament.Core.DTOs.Tournament;

namespace Tournament.Services
{
    public class TournamentService : ITournamentService
    {
        public Task DeleteTournament(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TournamentDto> GetTournamentAsync(int id, bool trackChanges = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TournamentDto>> GetTournamentsAsync(bool includeGames, bool trackChanges = false)
        {
            throw new NotImplementedException();
        }

        public Task PatchTorunament(int id, JsonPatchDocument<TournamentDto> patchDocument)
        {
            throw new NotImplementedException();
        }

        public Task PostTournament(TournamentDto dto)
        {
            throw new NotImplementedException();
        }

        public Task PutTournament(int id, TournamentDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
