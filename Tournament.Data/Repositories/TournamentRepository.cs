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
            return await _context.Tournaments.FindAsync(id);
        }

        public Task<bool> AnyAsync(int id)
        {
            throw new NotImplementedException();
        }


        public void Add(TournamentModel tournament)
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
