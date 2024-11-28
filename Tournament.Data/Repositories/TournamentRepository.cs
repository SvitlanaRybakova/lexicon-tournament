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

        public void Add(TournamentModel tournament)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(int id)
        {
            throw new NotImplementedException();
        }

       

        public Task<TournamentModel> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(TournamentModel tournament)
        {
            throw new NotImplementedException();
        }

        public void Update(TournamentModel tournament)
        {
            throw new NotImplementedException();
        }
    }
}
