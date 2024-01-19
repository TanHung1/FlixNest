using FlixNest.Models;
using Hangfire;
using Humanizer.Localisation;

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

        public void CreateDirector(Director director)
        {
            _context.Director.Add(director);
            _context.SaveChanges();
            BackgroundJob.Enqueue(() => SuccessfulCreation(director.DirId, director.Fname,director.LName, "Tạo thành công"));

        }

        public void DeleteDirector(int Id)
        {
            Director director = _context.Director.FirstOrDefault(x => x.DirId == Id);
            _context.Director.Remove(director);
            _context.SaveChanges(true);
            BackgroundJob.Enqueue(() => SuccessfulDeleted(director.DirId,  "Xóa thành công"));

        
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
                BackgroundJob.Enqueue(() => SuccessfulUpdate(director.DirId, director.Fname,director.LName, "Cập nhật thành công"));

            }
           
        }
        public void SuccessfulCreation(int id, string name, string lname,string des)
        {
            Director createDirector = _context.Director.FirstOrDefault(x => x.DirId == id);
        }
        public void SuccessfulUpdate(int dirId, string name, string lname,string des)
        {
            Director updateDirector = findbyId(dirId);
        }
        public void SuccessfulDeleted(int dirId, string des)
        {
            Director DeleteDirector = findbyId(dirId);
        }
    }
}
