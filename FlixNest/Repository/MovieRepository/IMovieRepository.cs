using FlixNest.Models;

namespace FlixNest.Repository.MovieRepository
{
    public interface IMovieRepository
    {
        public List<Movie> Getallwith();

        public List<Movie> GetAll();
        public List<Movie> getMovieDelete();

        public bool RestoreMovie(Movie movie);
        public Movie findbyId(int id);
        public bool CreateMovie(Movie movie, Episode episodes);
        public bool UpdateMovie(Movie movie);
        public bool DeleteMovie(int id);

        public bool DeleteCompleteMovie(int id);
        public List<Movie> findMoviebyName(string name);
        public Movie GetbyId(int id);
        public List<Movie> GetMoviebyYear(int id);
        public List<Movie> GetMoviebyFollower();
        public List<Movie> GetMoviebyCountry(int id);
        public List<Movie> GetMovieByGenreName(int gerneId);
        public List<Genre> GetGenresByMovieId(int movieId);

        public bool CheckNameMovie(string name);

        public int MovieCount();

        public List<Movie> GetMovieWithMostComment();
    }
}
