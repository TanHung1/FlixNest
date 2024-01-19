using FlixNest.Models;

namespace FlixNest.Repository.MovieDirectorRepository
{
    public class MovieDirectorRepository : IMovieDirectorRepository
    {
        private FlixNestDbContext _context;
        public MovieDirectorRepository(FlixNestDbContext context)
        {
            _context = context;
        }
        public void createMovieDirector(Movie movie, List<int> DirList)
        {
            foreach (var dirId in DirList)
            {
                MovieDirector movieDirector = new MovieDirector()
                {
                    DirId = dirId,
                    MovieId = movie.MovieId,
                    Director = _context.Director.Find(dirId),
                    Movie = movie
                };
                _context.MovieDirector.Add(movieDirector);
            }
            _context.SaveChanges();
            
        }

        public void updateMovieDirector(Movie movie, List<int> DirList)
        {
            //1.Lấy danh sách bảng MovieDirector
            var existDirectors = _context.MovieDirector.Where(x => x.MovieId == movie.MovieId).ToList();
            //2. Xóa những director không được chọn 
            foreach (var movieDirs in existDirectors)
            {
                if (!DirList.Contains(movieDirs.DirId))
                {
                    _context.MovieDirector.Remove(movieDirs);
                }
            }
            //3.Thêm những director được chọn 

            foreach (var dirId in DirList)
            {
                //kiểm tra xem director có trong ds ko
                if (!existDirectors.Any(x => x.DirId == dirId))
                {
                    _context.MovieDirector.Add(new MovieDirector { DirId = dirId, MovieId = movie.MovieId });
                }
            }
            _context.SaveChanges();
           
        }
    }
}
