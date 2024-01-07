using FlixNest.Models;

namespace FlixNest.Repository.LogMovieRepository
{
    public interface ILogMovieRepository
    {
        public List<MovieActivity> getAll();
    }
}
