using FlixNest.Models;
using Hangfire;
using Microsoft.EntityFrameworkCore;

namespace FlixNest.Repository.MovieRepository
{
    public class MovieRepository : IMovieRepository
    {
        private FlixNestDbContext _context;

        public bool False { get; private set; }

        public MovieRepository(FlixNestDbContext context)
        {
            _context = context;

        }
        private void AddMovieLog(Movie movie, string action)
        {
            MovieActivity log = new MovieActivity
            {
                MovieId = movie.MovieId,
                Action = action,
                DateTime = DateTime.Now,
                Description = $"{action} (MovieId: {movie.MovieId} Tên phim: {movie.MovieName})",
            };
            _context.MovieActivity.Add(log);
            _context.SaveChanges();

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

            AddMovieLog(movie, "Đã được tạo");
            BackgroundJob.Enqueue(() => SuccessfulCreation(movie.MovieId, "Tạo thành công"));
            return true;
        }
        public bool DeleteCompleteMovie(int id)
        {
            Movie movie = _context.Movie.FirstOrDefault(x => x.MovieId == id);
            AddMovieLog(movie, "Đã xóa vĩnh viễn");
            _context.Movie.Remove(movie);
            _context.SaveChanges();
            return true;
        }
        public bool DeleteMovie(int id)
        {
            Movie movie = _context.Movie.FirstOrDefault(x => x.MovieId == id);
            if (movie != null)
            {
                movie.IsDeleted = true;

                //_context.Movie.Remove(movie);
                AddMovieLog(movie, "Đã xóa phim");
                _context.SaveChanges(true);
                BackgroundJob.Enqueue(() => SuccessfulDeleted(movie.MovieId, "Xóa thành công"));
            }

            return true;
        }

        public Movie findbyId(int id)
        {
            Movie movie = _context.Movie.FirstOrDefault(x => x.MovieId == id);
            return movie;
        }

        public List<Movie> Getallwith()
        {
            return _context.Movie
                                  .Where(x => !x.IsDeleted)
                                  .Include(x => x.movieGenres)
                                  .Include(x => x.movieActors)
                                  .Include(x => x.movieDirectors)
                                  .ToList();
        }

        public List<Movie> GetAll()
        {
            return _context.Movie.Where(x => !x.IsDeleted).ToList();
        }
        public List<Movie> getMovieDelete()
        {
            return _context.Movie.Where(x => x.IsDeleted).ToList();
        }
        public bool RestoreMovie(Movie movie)
        {
            Movie mov = _context.Movie.FirstOrDefault(x => x.MovieId == movie.MovieId);
            if (mov != null)
            {
                mov.MovieName = movie.MovieName;
                mov.MovieTitle = movie.MovieTitle;
                mov.MovieTime = movie.MovieTime;
                mov.YearId = movie.YearId;
                mov.CountryId = movie.CountryId;
                mov.Image = movie.Image;
                mov.IsDeleted = false;

                _context.SaveChanges();

                AddMovieLog(movie, "Đã khôi phục bộ phim");
            }
            return true;

        }
        public bool UpdateMovie(Movie movie)
        {
            //string desc;
            Movie mov = _context.Movie.FirstOrDefault(x => x.MovieId == movie.MovieId);

            if (movie != null)
            {
                mov.MovieName = movie.MovieName;
                mov.MovieTitle = movie.MovieTitle;
                mov.MovieTime = movie.MovieTime;
                mov.YearId = movie.YearId;
                mov.CountryId = movie.CountryId;
                mov.Image = movie.Image;

                _context.SaveChanges();

                AddMovieLog(movie, "Đã cập nhật phim");
                BackgroundJob.Enqueue(() => SuccessfulUpdate(movie.MovieId, "Cập nhật thành công"));
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

        public void SuccessfulCreation(int id, string des)
        {
            Movie createMovie = _context.Movie.FirstOrDefault(x => x.MovieId == id);
        }
        public void SuccessfulUpdate(int MovieId, string des)
        {
            Movie updateMovie = findbyId(MovieId);
        }
        public void SuccessfulDeleted(int MovieId, string des)
        {
            Movie DeleteMovie = findbyId(MovieId);
        }
    }
}
