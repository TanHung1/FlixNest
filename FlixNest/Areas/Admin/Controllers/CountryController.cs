using FlixNest.Models;
using FlixNest.Repository.CountryRepository;
using Microsoft.AspNetCore.Mvc;

namespace FlixNest.Areas.Admin.Controllers
{
    [Area("admin")]
    public class CountryController : Controller
    {
        private ICountryRepository _countryRepository;
        public CountryController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public IActionResult CreateCountry()
        {
            return View("CreateCountry", new Country());
        }
        [HttpPost]
        public IActionResult saveCountry(Country country)
        {
            _countryRepository.CreateCountry(country);
            return RedirectToAction("Table", "Table");
        }
        public IActionResult UpdateCountry(int id)
        {
            return View("UpdateCountry", _countryRepository.findbyId(id));

        }
        [HttpPost]
        public IActionResult EditCountry(Country country)
        {
            _countryRepository.UpdateCountry(country);
            return RedirectToAction("Table", "Table");
        }
        public IActionResult DeleteCountry(int id)
        {
            _countryRepository.DeleteCountry(id);
            return RedirectToAction("Table", "Table");
        }
    }
}
