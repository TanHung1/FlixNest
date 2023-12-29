using FlixNest.Models;

namespace FlixNest.Repository.DirectorRepository
{
    public interface IDirectorRepository
    {
        public List<Director> GetAll();

        public Dictionary<int, string> GetAllDirector();
        public Director findbyId(int Id);

        public bool CreateDirector(Director director);

        public bool UpdateDirector(Director director);
        public bool DeleteDirector(int Id);

        public bool CheckNameActor(string firstname, string lastname);
    }
}
