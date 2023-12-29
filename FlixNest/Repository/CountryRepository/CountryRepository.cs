using FlixNest.Models;

namespace FlixNest.Repository.CountryRepository
{
    public class CountryRepository : ICountryRepository
    {
        private FlixNestDbContext _context;
        public CountryRepository(FlixNestDbContext context)
        {
            _context = context;
        }

        public bool CheckNameCountry(string name)
        {
            Country country = _context.Countries.Where(x => x.CountryName.Trim() == name.Trim()).FirstOrDefault();
            if (country == null)
            {
                return false;
            }
            return true;
        }

        public bool CreateCountry(Country country)
        {
            _context.Countries.Add(country);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteCountry(int id)
        {
            Country countries = _context.Countries.FirstOrDefault(x => x.CountryId == id);
            _context.Countries.Remove(countries);
            _context.SaveChanges();
            return true;
        }

        public Country findbyId(int id)
        {
            Country countries = _context.Countries.FirstOrDefault(x => x.CountryId == id);
            return countries;
        }

        public List<Country> GetAll()
        {
            return _context.Countries.ToList();
        }

        public bool UpdateCountry(Country country)
        {
            Country countries = _context.Countries.FirstOrDefault(x => x.CountryId == country.CountryId);
            if (country != null)
            {
                countries.CountryName = country.CountryName;
                _context.SaveChanges();
            }
            return true;

        }
    }
}
