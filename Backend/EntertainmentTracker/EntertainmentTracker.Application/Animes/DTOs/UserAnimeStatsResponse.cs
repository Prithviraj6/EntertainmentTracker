
namespace EntertainmentTracker.Application.Animes.DTOs
{
    public sealed class UserAnimeStatsResponse
    {
        public int PlanToWatch { get; set; }

        public int Watching { get; set; }

        public int Completed { get; set; }

        public int OnHold { get; set; }

        public int Dropped { get; set; }
    }
}
