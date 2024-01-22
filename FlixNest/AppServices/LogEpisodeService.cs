using FlixNest.IAppServices;
using FlixNest.Models;

namespace FlixNest.AppServices
{
    public class LogEpisodeService : ILogEpisodeService
    {
        private readonly FlixNestDbContext _context;
        public LogEpisodeService(FlixNestDbContext context)
        {
            _context = context;
        }
        public List<EpisodeActivity> getALl()
        {
            return _context.EpisodeActivity.ToList();
        }

    }
}
