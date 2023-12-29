using FlixNest.Models;

namespace FlixNest.Repository.GenreRepository
{
    public interface IGenreRepository
    {
        public List<Genre> GetAll();

        Dictionary<int, string> GetAllGenreNames();

        public Genre findbyId(int id);

        public bool CreateGenre(Genre genre);
        public bool UpdateGenre(Genre genre);
        public bool DeleteGenre(int id);
        public bool CheckNameGenre(string name);
    }
}
