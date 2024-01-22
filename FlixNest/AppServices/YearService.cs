using FlixNest.IAppServices;
using FlixNest.Models;
using Hangfire;

namespace FlixNest.AppServices
{
    public class YearService : IYearService
    {
        private FlixNestDbContext _context;
        public YearService(FlixNestDbContext context)
        {
            _context = context;
        }
        public void CreateYear(Year year)
        {
            _context.Years.Add(year);
            _context.SaveChanges();

        }

        public void DeleteYear(int id)
        {
            Year year = _context.Years.FirstOrDefault(x => x.YearId == id);
            _context.Years.Remove(year);
            _context.SaveChanges();

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

        public void UpdateYear(Year year)
        {
            Year years = _context.Years.FirstOrDefault(x => x.YearId == year.YearId);
            if (year != null)
            {
                years.YearName = year.YearName;
                _context.SaveChanges();
            }

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
