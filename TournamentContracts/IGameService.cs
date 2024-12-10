using Tournament.Core.DTOs.Game;
using Microsoft.AspNetCore.JsonPatch;


namespace Tournament.Contracts
{
    public interface IGameService
    {
        Task<IEnumerable<GameDto>> GetGamesAsync(bool trackChanges = false);
        Task<GameDto> GetGameAsync(string id, bool trackChanges = false);
        Task PostGame(GameDto dto);
        Task PutGame(int id, GameDto dto);
        Task DeleteGame(int id);
        Task PatchGame(int id, JsonPatchDocument<GameDto> patchDocument);
    }
}
