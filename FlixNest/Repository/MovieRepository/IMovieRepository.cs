using FlixNest.Models;

namespace FlixNest.Repository.MovieRepository
{
    public interface IMovieRepository
    {
        public List<Movie> Getallwith();

        public List<Movie> GetAll();
        public List<Movie> getMovieDelete();

        public void RestoreMovie(Movie movie);
        public Movie findbyId(int id);
        public void CreateMovie(Movie movie, Episode episodes);
        public void ApproveMovie(Movie movie);
        public void RejectMovie(Movie movie);
        public void UpdateMovie(Movie movie);
        public void DeleteMovie(int id);

        public void DeleteCompleteMovie(int id);
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

        public List<MovieActivity> GetMovieActivities(int id);
    }
}
