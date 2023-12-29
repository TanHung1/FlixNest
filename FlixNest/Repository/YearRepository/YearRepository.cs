using FlixNest.Models;

namespace FlixNest.Repository.YearRepository
{
    public class YearRepository : IYearRepository
    {
        private FlixNestDbContext _context;
        public YearRepository(FlixNestDbContext context)
        {
            _context = context;
        }
        public bool CreateYear(Year year)
        {
            _context.Years.Add(year);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteYear(int id)
        {
            Year year = _context.Years.FirstOrDefault(x => x.YearId == id);
            _context.Years.Remove(year);
            _context.SaveChanges();
            return true;
        }

        public List<Year> GetAll()
        {
            return _context.Years.ToList();
        }

        public Year findbyId(int id)
        {
            Year year = _context.Years.FirstOrDefault(x => x.YearId == id);
            return year;
        }

        public bool UpdateYear(Year year)
        {
            Year years = _context.Years.FirstOrDefault(x => x.YearId == year.YearId);
            if (year != null)
            {
                years.YearName = year.YearName;
                _context.SaveChanges();
            }
            return true;
        }

        public bool CheckYear(string name)
        {
            Year year = _context.Years.Where(x => x.YearName.Trim() == name.Trim()).FirstOrDefault();
            if (year != null)
            {
                return true;
            }
            return false;
        }
    }
}
