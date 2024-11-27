using Tournament.Data;
using Tournament.Data.Data;

namespace Tournament.API.Extention
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task<IApplicationBuilder> SeedDataAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<TournamentContext>();
                try
                {
                    await SeedData.Init(context, services);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during seeding: {ex.Message}");
                    throw;
                }
            }
            return app;
        }
    }
}
