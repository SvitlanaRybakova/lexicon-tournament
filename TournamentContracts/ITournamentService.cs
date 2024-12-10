using Microsoft.AspNetCore.JsonPatch;
using Tournament.Core.DTOs.Tournament;

namespace Tournament.Contracts
{
    public interface ITournamentService
    {
        Task<IEnumerable<TournamentDto>> GetTournamentsAsync(bool includeGames, bool trackChanges = false);
        Task<TournamentDto> GetTournamentAsync(int id, bool trackChanges = false);
        Task PostTournament(TournamentDto dto);
        Task PutTournament(int id, TournamentDto dto);
        Task DeleteTournament(int id);
        Task PatchTorunament(int id, JsonPatchDocument<TournamentDto> patchDocument);
    }
}
