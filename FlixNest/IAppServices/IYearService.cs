using FlixNest.Models;

namespace FlixNest.IAppServices
{
    public interface IYearService
    {
        public List<Year> GetAll();
        public Year findbyId(int id);


        public void CreateYear(Year year);
        public void UpdateYear(Year year);
        public void DeleteYear(int id);
        public bool CheckYear(string name);
    }
}
