
namespace Tournament.Core.DTOs.Tournament
{
    public class TournamentDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate => StartDate.AddMonths(3); // 3 mounth afer start date
    }

    public class CreateTournamentDto
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
    }
}
