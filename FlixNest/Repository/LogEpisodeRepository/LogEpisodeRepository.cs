using FlixNest.Models;

namespace FlixNest.Repository.LogEpisodeRepository
{
    public class LogEpisodeRepository : ILogEpisodeRepository
    {
        private readonly FlixNestDbContext _context;
        public LogEpisodeRepository(FlixNestDbContext context)
        {
            _context = context;
        }
        public List<EpisodeActivity> getALl()
        {
            return _context.EpisodeActivity.ToList();
        }

    }
}
