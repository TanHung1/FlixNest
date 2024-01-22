using FlixNest.IAppServices;
using FlixNest.Models;
using Hangfire;
using Humanizer.Localisation;

namespace FlixNest.AppServices
{
    public class DirectorService : IDirectorService
    {
        private FlixNestDbContext _context;

        public DirectorService(FlixNestDbContext context)
        {
            _context = context;
        }

        public bool CheckNameActor(string firstname, string lastname)
        {
            Director director = _context.Director.Where(x => x.Fname.Trim() == firstname.Trim()
                                 && x.LName.Trim() == lastname.Trim()).FirstOrDefault();
            if (director == null)
            {
                return false;
            }
            return true;
        }

        public void CreateDirector(Director director)
        {
            _context.Director.Add(director);
            _context.SaveChanges();

        }

        public void DeleteDirector(int Id)
        {
            Director director = _context.Director.FirstOrDefault(x => x.DirId == Id);
            _context.Director.Remove(director);
            _context.SaveChanges(true);


        }

        public Director findbyId(int Id)
        {
            Director director = _context.Director.FirstOrDefault(x => x.DirId == Id);
            return director;
        }

        public List<Director> GetAll()
        {
            return _context.Director.ToList();
        }

        public Dictionary<int, string> GetAllDirector()
        {
            return _context.Director.ToDictionary(x => x.DirId, x => x.Fname + " " + x.LName);
        }

        public void UpdateDirector(Director director)
        {
            Director dir = _context.Director.FirstOrDefault(x => x.DirId == director.DirId);
            if (dir != null)
            {
                dir.Fname = director.Fname;
                dir.LName = director.LName;
                _context.SaveChanges();

            }

        }

    }
}
