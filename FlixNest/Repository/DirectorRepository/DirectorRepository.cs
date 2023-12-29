using FlixNest.Models;

namespace FlixNest.Repository.DirectorRepository
{
    public class DirectorRepository : IDirectorRepository
    {
        private FlixNestDbContext _context;

        public DirectorRepository(FlixNestDbContext context)
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

        public bool CreateDirector(Director director)
        {
            _context.Director.Add(director);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteDirector(int Id)
        {
            Director director = _context.Director.FirstOrDefault(x => x.DirId == Id);
            _context.Director.Remove(director);
            _context.SaveChanges(true);
            return true;
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

        public bool UpdateDirector(Director director)
        {
            Director dir = _context.Director.FirstOrDefault(x => x.DirId == director.DirId);
            if (dir != null)
            {
                dir.Fname = director.Fname;
                dir.LName = director.LName;

                _context.SaveChanges();
            }
            return true;
        }
    }
}
