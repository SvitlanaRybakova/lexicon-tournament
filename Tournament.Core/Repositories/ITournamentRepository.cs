﻿using Tournament.Core.Entities;

namespace Tournament.Core.Repositories
{
    public interface ITournamentRepository
    {
        Task<IEnumerable<TournamentModel>> GetAllAsync();
        Task<TournamentModel> GetAsync(int id);
        Task<bool> AnyAsync(int id);
        void Add(TournamentModel tournament);
        void Update(TournamentModel tournament);
        void Remove(TournamentModel tournament);
    }
}
