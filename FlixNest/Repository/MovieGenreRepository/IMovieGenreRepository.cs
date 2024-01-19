using FlixNest.Models;

namespace FlixNest.Repository.MovieGenreRepository
{
    public interface IMovieGenreRepository
    {
        List<MovieGenre> GetAllMovieGenre();

        public MovieGenre findbyId(int movieId, int genreId);

        public void updateMovieGenre(Movie movie, List<int> GenreList);

        public void createMovieGenre(Movie movie, List<int> GenreList);

    }
}
