using FlixNest.Models;

namespace FlixNest.IAppServices
{
    public interface ILogMovieService
    {
        public List<MovieActivity> getAll();
        public List<MovieActivity> getById(int id);
    }
}
