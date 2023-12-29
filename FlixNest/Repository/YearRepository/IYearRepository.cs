using FlixNest.Models;

namespace FlixNest.Repository.YearRepository
{
    public interface IYearRepository
    {
        public List<Year> GetAll();
        public Year findbyId(int id);


        public bool CreateYear(Year year);
        public bool UpdateYear(Year year);
        public bool DeleteYear(int id);
        public bool CheckYear(string name);
    }
}
