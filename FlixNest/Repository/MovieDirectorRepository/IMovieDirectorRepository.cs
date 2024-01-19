using FlixNest.Models;

namespace FlixNest.Repository.MovieDirectorRepository
{
    public interface IMovieDirectorRepository
    {
        public void createMovieDirector(Movie movie, List<int> DirList);
        public void updateMovieDirector(Movie movie, List<int> DirList);
    }
}
