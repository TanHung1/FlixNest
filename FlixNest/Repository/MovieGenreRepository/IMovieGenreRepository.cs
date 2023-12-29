using FlixNest.Models;

namespace FlixNest.Repository.MovieGenreRepository
{
    public interface IMovieGenreRepository
    {
        List<MovieGenre> GetAllMovieGenre();

        public MovieGenre findbyId(int movieId, int genreId);

        public bool updateMovieGenre(Movie movie, List<int> GenreList);

        public bool createMovieGenre(Movie movie, List<int> GenreList);

    }
}
