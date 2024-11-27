using Microsoft.EntityFrameworkCore;
using Tournament.Core.Entities;

namespace Tournament.Data.Data
{
    public class TournamentContext : DbContext
    {
        public TournamentContext (DbContextOptions<TournamentContext> options)
            : base(options)
        {
        }

        public DbSet<Game> Games { get; set; } = default!;
        public DbSet<TournamentModel> Tournaments { get; set; } = default!;
    }
}
