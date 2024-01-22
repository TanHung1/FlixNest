using FlixNest.IAppServices;
using FlixNest.Models;

namespace FlixNest.AppServices
{
    public class LogMovieService : ILogMovieService
    {
        private readonly FlixNestDbContext _context;

        public LogMovieService(FlixNestDbContext context)
        {
            _context = context;
        }

        public List<MovieActivity> getAll()
        {
            return _context.MovieActivity.ToList();
        }

        public List<MovieActivity> getById(int id)
        {
            List<MovieActivity> movieActivities = _context.MovieActivity.Where(x => x.MovieId == id).ToList();
            return movieActivities;
        }
    }
}
