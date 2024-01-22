using FlixNest.Models;

namespace FlixNest.IAppServices
{
    public interface IMovieDirectorService
    {
        public void createMovieDirector(Movie movie, List<int> DirList);
        public void updateMovieDirector(Movie movie, List<int> DirList);
    }
}
