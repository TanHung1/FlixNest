using FlixNest.Models;

namespace FlixNest.Repository.GenreRepository
{
    public interface IGenreRepository
    {
        public List<Genre> GetAll();

        Dictionary<int, string> GetAllGenreNames();

        public Genre findbyId(int id);

        public bool CheckGenreUsed(int id);

        public void CreateGenre(Genre genre);
        public void UpdateGenre(Genre genre);
        public void DeleteGenre(int id);
        public bool CheckNameGenre(string name);
    }
}
