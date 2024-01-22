using FlixNest.IAppServices;
using FlixNest.Models;
using Hangfire;
using System.Diagnostics.Metrics;

namespace FlixNest.AppServices
{
    public class CountryService : ICountryService
    {
        private FlixNestDbContext _context;
        public CountryService(FlixNestDbContext context)
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

        public void CreateCountry(Country country)
        {
            _context.Countries.Add(country);
            _context.SaveChanges();


        }

        public void DeleteCountry(int id)
        {
            Country countries = _context.Countries.FirstOrDefault(x => x.CountryId == id);
            _context.Countries.Remove(countries);
            _context.SaveChanges();

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

        public void UpdateCountry(Country country)
        {
            Country countries = _context.Countries.FirstOrDefault(x => x.CountryId == country.CountryId);
            if (country != null)
            {
                countries.CountryName = country.CountryName;
                _context.SaveChanges();

            }


        }


    }
}
