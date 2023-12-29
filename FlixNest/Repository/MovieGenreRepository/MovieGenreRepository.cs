using FlixNest.Models;

namespace FlixNest.Repository.MovieGenreRepository
{
    public class MovieGenreRepository : IMovieGenreRepository
    {
        private FlixNestDbContext _context;
        public MovieGenreRepository(FlixNestDbContext context)
        {
            _context = context;
        }

        public bool createMovieGenre(Movie movie, List<int> GenreList)
        {
            foreach (var genreId in GenreList)
            {
                MovieGenre movieGenre = new MovieGenre()
                {
                    GenreId = genreId,
                    MovieId = movie.MovieId,
                    Genre = _context.Genre.Find(genreId),
                    Movie = movie
                };
                _context.MovieGenre.Add(movieGenre);
                _context.SaveChanges();
            }
            return true;
        }

        public MovieGenre findbyId(int movieId, int genreId)
        {
            MovieGenre movieGenre = _context.MovieGenre.FirstOrDefault(
                x => x.MovieId == movieId && x.GenreId == genreId);
            return movieGenre;
        }

        public List<MovieGenre> GetAllMovieGenre()
        {
            return _context.MovieGenre.ToList();
        }

        public bool updateMovieGenre(Movie movie, List<int> GenreList)
        {
            //lấy danh sách thể loại hiện tại của phim
            var existingGenres = _context.MovieGenre.Where(x => x.MovieId == movie.MovieId).ToList();

            // Xóa những mối quan hệ không còn được chọn từ form
            foreach (var movieGenre in existingGenres)
            {
                if (!GenreList.Contains(movieGenre.GenreId))
                {
                    _context.MovieGenre.Remove(movieGenre);
                }
            }

            foreach (var genreId in GenreList)
            {
                // kiểm tra xem thể loại có trong danh sách không
                if (!existingGenres.Any(x => x.GenreId == genreId))
                {
                    //Nếu không có thì thêm vào
                    _context.MovieGenre.Add(new MovieGenre { GenreId = genreId, MovieId = movie.MovieId });
                }

            }
            _context.SaveChanges();

            return true;

        }
    }
}
