using FlixNest.Models;
using Hangfire;

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
            BackgroundJob.Enqueue(() => SuccessfulCreation(year.YearId, "Tạo thành công"));
            return true;
        }

        public bool DeleteYear(int id)
        {
            Year year = _context.Years.FirstOrDefault(x => x.YearId == id);
            _context.Years.Remove(year);
            _context.SaveChanges();
            BackgroundJob.Enqueue(() => SuccessfulDeleted(year.YearId, "Xóa thành công"));
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
                BackgroundJob.Enqueue(() => SuccessfulUpdate(year.YearId, "Cập nhật thành công"));
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

        public void SuccessfulCreation(int id, string des)
        {
            Year createYear = _context.Years.FirstOrDefault(x => x.YearId == id);
        }
        public void SuccessfulUpdate(int id, string des)
        {
            Year updateYear = findbyId(id);
        }
        public void SuccessfulDeleted(int id, string des)
        {
            Year deleteyear = findbyId(id);
        }
    }
}
