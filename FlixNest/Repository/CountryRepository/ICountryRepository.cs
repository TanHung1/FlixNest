using FlixNest.Models;

namespace FlixNest.Repository.CountryRepository
{
    public interface ICountryRepository
    {
        public List<Country> GetAll();
        public Country findbyId(int id);

        public bool CreateCountry(Country country);
        public bool UpdateCountry(Country country);
        public bool DeleteCountry(int id);

        public bool CheckNameCountry(string name);

    }
}
