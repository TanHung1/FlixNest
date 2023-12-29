using FlixNest.Models;

namespace FlixNest.Repository.GenreRepository
{
    public class GenreRepository : IGenreRepository
    {
        private FlixNestDbContext _context;

        public GenreRepository(FlixNestDbContext context)
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

        public bool CreateGenre(Genre genre)
        {
            _context.Genre.Add(genre);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteGenre(int id)
        {
            Genre genre = _context.Genre.FirstOrDefault(x => x.GenreId == id);
            _context.Genre.Remove(genre);
            _context.SaveChanges();
            return true;
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

        public bool UpdateGenre(Genre genre)
        {
            Genre ge = _context.Genre.FirstOrDefault(x => x.GenreId == genre.GenreId);
            if (ge != null)
            {
                ge.GenreName = genre.GenreName;
                _context.SaveChanges();
            }
            return true;
        }
    }
}
