using FlixNest.Models;
using Microsoft.EntityFrameworkCore;

namespace FlixNest.Repository.MovieRepository
{
    public class MovieRepository : IMovieRepository
    {
        private FlixNestDbContext _context;

        public MovieRepository(FlixNestDbContext context)
        {
            _context = context;

        }

        public bool CreateMovie(Movie movie, Episode episode)
        {
            _context.Movie.Add(movie);
            _context.SaveChanges();

            _context.episodes.Add(episode);
            episode.EpisodeName = "Tập PV";
            episode.Video = "Chưa có";
            episode.ReleaseDate = DateTime.Now;
            episode.MovieId = movie.MovieId;
            _context.SaveChanges();

            return true;
        }

        public bool DeleteMovie(int id)
        {
            Movie movie = _context.Movie.FirstOrDefault(x => x.MovieId == id);
            /*
             * 
             */
            _context.Movie.Remove(movie);
            _context.SaveChanges(true);
            return true;
        }

        public Movie findbyId(int id)
        {
            Movie movie = _context.Movie.FirstOrDefault(x => x.MovieId == id);
            return movie;
        }

        public List<Movie> Getallwith()
        {
            return _context.Movie.Include(x => x.movieGenres)
                                  .Include(x => x.movieActors)
                                  .Include(x => x.movieDirectors)
                                  .ToList();
        }

        public List<Movie> GetAll()
        {
            return _context.Movie.ToList();
        }

        public bool UpdateMovie(Movie movie)
        {
            //string desc;
            Movie mov = _context.Movie.FirstOrDefault(x => x.MovieId == movie.MovieId);

            //if (newDataMovie.MovieName != currentDataMovie.MovieName)
            //{
            //    desc += currentDataMovie.MovieName "->" newDataMovie.MovieName;
            //}

            //if (newDataMovie.MovieTitle != currentDataMovie.MovieTitle)
            //{
            //    desc += currentDataMovie.MovieName "->" newDataMovie.MovieName;
            //}
            if (movie != null)
            {
                mov.MovieName = movie.MovieName;
                mov.MovieTitle = movie.MovieTitle;
                mov.MovieTime = movie.MovieTime;
                mov.YearId = movie.YearId;
                mov.CountryId = movie.CountryId;
                mov.Image = movie.Image;

                _context.SaveChanges();
            }
            return true;
        }

        public Movie GetbyId(int id)
        {
            return _context.Movie.FirstOrDefault(x => x.MovieId == id);
        }

        public List<Genre> GetGenresByMovieId(int movieId)
        {
            return _context.MovieGenre.Where(x => x.MovieId == movieId).Select(x => x.Genre).ToList();
        }

        public List<Movie> GetMoviebyYear(int id)
        {
            return _context.Movie.Where(x => x.YearId == id).ToList();
        }

        public List<Movie> GetMovieByGenreName(int gerneId)
        {
            return _context.Movie.Include(x => x.movieGenres).ThenInclude(x => x.Genre)
                                 .Where(x => x.movieGenres.Any(x => x.Genre.GenreId == gerneId)).ToList();
        }

        public List<Movie> findMoviebyName(string name)
        {
            return _context.Movie.Where(x => x.MovieName.Contains(name)).ToList();
        }

        public List<Movie> GetMoviebyCountry(int id)
        {
            return _context.Movie.Where(x => x.CountryId == id).ToList();
        }

        public List<Movie> GetMoviebyFollower()
        {
            return _context.Movie.OrderByDescending(x => x.FollowerCount).ToList();
        }

        public bool CheckNameMovie(string name)
        {
            Movie movie = _context.Movie.Where(x => x.MovieName.Trim() == name.Trim()).FirstOrDefault();
            if (movie == null)
            {
                return false;
            }
            return true;
        }

        public int MovieCount()
        {
            return _context.Movie.Count();
        }

        public List<Movie> GetMovieWithMostComment()
        {
            return _context.Movie.OrderByDescending(x => x.MovieComments.Count).ToList();
        }
    }
}
