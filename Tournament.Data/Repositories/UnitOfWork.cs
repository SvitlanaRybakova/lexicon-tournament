using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using Tournament.Data.Data;

namespace Tournament.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TournamentContext _context;

        public UnitOfWork(TournamentContext context)
        {
            _context = context;
            TournamentRepository = new TournamentRepository(_context);
            GameRepository = new GameRepository(_context);
        }

        public ITournamentRepository TournamentRepository { get; }
        public IGameRepository GameRepository { get; }

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
