using FlixNest.Models;

namespace FlixNest.Repository.MovieDirectorRepository
{
    public interface IMovieDirectorRepository
    {
        public bool createMovieDirector(Movie movie, List<int> DirList);
        public bool updateMovieDirector(Movie movie, List<int> DirList);
    }
}
