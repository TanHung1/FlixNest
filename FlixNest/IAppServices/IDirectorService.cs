using FlixNest.Models;

namespace FlixNest.IAppServices
{
    public interface IDirectorService
    {
        public List<Director> GetAll();

        public Dictionary<int, string> GetAllDirector();
        public Director findbyId(int Id);

        public void CreateDirector(Director director);

        public void UpdateDirector(Director director);
        public void DeleteDirector(int Id);

        public bool CheckNameActor(string firstname, string lastname);
    }
}
