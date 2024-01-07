using FlixNest.Models;
using Hangfire;
using System.Diagnostics.Metrics;

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
            BackgroundJob.Enqueue(() => SuccessfulCreation(country.CountryId, country.CountryName, "Tạo thành công"));

            return true;
        }

        public bool DeleteCountry(int id)
        {
            Country countries = _context.Countries.FirstOrDefault(x => x.CountryId == id);
            _context.Countries.Remove(countries);
            _context.SaveChanges();
            BackgroundJob.Enqueue(() => SuccessfulDeleted(countries.CountryId, "Xóa thành công"));
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
                BackgroundJob.Enqueue(() => SuccessfulUpdate(country.CountryId, country.CountryName, "Cập nhật thành công"));

            }
            return true;

        }

        public void SuccessfulCreation(int id, string name, string des)
        {
            Country createCountry = _context.Countries.FirstOrDefault(x => x.CountryId == id);
        }
        public void SuccessfulUpdate(int countryId, string name, string des)
        {
            Country updateCountry = findbyId(countryId);
        }
        public void SuccessfulDeleted(int countryId, string des)
        {
            Country DeleteCountry = findbyId(countryId);
        }
    }
}
