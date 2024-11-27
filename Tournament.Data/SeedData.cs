using Tournament.Data.Data;
using Tournament.Core.Entities;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace Tournament.Data
{
    public class SeedData
    {
        private static TournamentContext context = default!;

        public static async Task Init(TournamentContext _context, IServiceProvider services)
        {
            context = _context;

            if (!await context.Tournaments.AnyAsync())
            {
                await SeedTournamentsAndGames();
            }
        }

        private static async Task SeedTournamentsAndGames()
        {
            var fakerTournament = new Faker<TournamentModel>()
                .RuleFor(t => t.Title, f => f.Commerce.ProductName())  // Random tournament name
                .RuleFor(t => t.StartDate, f => f.Date.Future())      // Random future start date
                .RuleFor(t => t.Games, f => GenerateGames(f, 3));      // Generate 3 random games

            var tournaments = fakerTournament.Generate(5);  // Generate 5 tournaments

            // Add tournaments and games to the context
            await context.Tournaments.AddRangeAsync(tournaments);
            await context.SaveChangesAsync();
        }

        // Helper method to generate random games for a tournament
        private static List<Game> GenerateGames(Faker f, int count)
        {
            var fakerGame = new Faker<Game>()
                .RuleFor(g => g.Title, f => f.Commerce.ProductName())  // Random game name
                .RuleFor(g => g.Description, f => f.Lorem.Sentence())  // Random description
                .RuleFor(g => g.Time, f => f.Date.Future())           // Random future game time
                .RuleFor(g => g.TournamentId, f => f.Random.Int());   // Random TournamentId

            return fakerGame.Generate(count);  // Generate the specified number of games
        }
    }
}
