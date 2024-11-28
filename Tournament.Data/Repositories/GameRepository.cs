﻿using Microsoft.EntityFrameworkCore;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using Tournament.Data.Data;

namespace Tournament.Data.Repositories
{
    public class GameRepository: IGameRepository
    {
        private readonly TournamentContext _context;

        public GameRepository(TournamentContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _context.Games.ToListAsync();
        }


        public async Task<Game> GetAsync(int id)
        {
            return await _context.Games.FindAsync(id);
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await _context.Games.AnyAsync(t => t.Id == id);
        }

        public void Add(Game game)
        {
            _context.Games.Add(game);
        }

        public void Remove(Game game)
        {
            _context.Games.Remove(game);
        }

        public void Update(Game game)
        {
            _context.Games.Update(game);
        }
    }
}
