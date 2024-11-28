using Microsoft.EntityFrameworkCore;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using Tournament.Data.Data;

namespace Tournament.Data.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly TournamentContext _context;

        public TournamentRepository(TournamentContext context)
        {
            _context = context;
        }

        public  async Task<IEnumerable<TournamentModel>> GetAllAsync()
        {

            return await _context.Tournaments.Include(t => t.Games).ToListAsync();
        }

        public async Task<TournamentModel> GetAsync(int id)
        {
            return await _context.Tournaments
                         .Include(t => t.Games)
                         .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await _context.Tournaments.AnyAsync(t => t.Id == id);
        }


        public void Add(TournamentModel tournament)
        {
              _context.Tournaments.Add(tournament);
        }

        public void Remove(TournamentModel tournament)
        {
            _context.Tournaments.Remove(tournament);
        }

        public void Update(TournamentModel tournament)
        {
            _context.Tournaments.Update(tournament);
        }
    }
}
