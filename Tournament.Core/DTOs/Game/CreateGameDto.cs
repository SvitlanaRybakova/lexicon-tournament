namespace Tournament.Core.DTOs.Game
{
    public class CreateGameDto
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public string Description { get; set; }

        // FK
        public int TournamentModelId { get; set; }
    }
}
