using FlixNest.Models;

namespace FlixNest.Repository.CountryRepository
{
    public interface ICountryRepository
    {
        public List<Country> GetAll();
        public Country findbyId(int id);

        public void CreateCountry(Country country);
        public void UpdateCountry(Country country);
        public void DeleteCountry(int id);

        public bool CheckNameCountry(string name);

    }
}
