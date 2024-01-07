using FlixNest.Models;

namespace FlixNest.Repository.LogMovieRepository
{
    public class LogMovieRepository : ILogMovieRepository
    {
        private readonly FlixNestDbContext _context;

        public LogMovieRepository(FlixNestDbContext context)
        {
            _context = context;
        }

        public List<MovieActivity> getAll()
        {
            return _context.MovieActivity.ToList();
        }
    }
}
