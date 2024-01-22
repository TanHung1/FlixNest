using FlixNest.IAppServices;
using FlixNest.Models;

using Hangfire;

namespace FlixNest.AppServices
{
    public class GenreService : IGenreService
    {
        private FlixNestDbContext _context;

        public GenreService(FlixNestDbContext context)
        {
            _context = context;

        }

        public bool CheckNameGenre(string name)
        {
            Genre genre = _context.Genre.Where(x => x.GenreName.Trim() == name.Trim()).FirstOrDefault();
            if (genre == null)
            {
                return false;
            }
            return true;
        }

        public void CreateGenre(Genre genre)
        {
            _context.Genre.Add(genre);
            _context.SaveChanges();


        }

        public void DeleteGenre(int id)
        {
            Genre genre = _context.Genre.FirstOrDefault(x => x.GenreId == id);
            bool isGenreUsed = _context.MovieGenre.Any(x => x.GenreId == id);

            _context.Genre.Remove(genre);
            _context.SaveChanges();


        }
        public bool CheckGenreUsed(int id)
        {
            bool isGenreUsed = _context.MovieGenre.Any(x => x.GenreId == id);
            return isGenreUsed;
        }
        public Genre findbyId(int id)
        {
            Genre genre = _context.Genre.FirstOrDefault(x => x.GenreId == id);
            return genre;
        }

        public List<Genre> GetAll()
        {
            return _context.Genre.ToList();
        }

        public Dictionary<int, string> GetAllGenreNames()
        {
            return _context.Genre.ToDictionary(g => g.GenreId, g => g.GenreName);
        }

        public void UpdateGenre(Genre genre)
        {
            Genre ge = _context.Genre.FirstOrDefault(x => x.GenreId == genre.GenreId);
            if (ge != null)
            {
                ge.GenreName = genre.GenreName;
                _context.SaveChanges();

            }

        }

    }
}
