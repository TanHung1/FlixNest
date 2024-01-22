using FlixNest.IAppServices;
using FlixNest.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlixNest.Areas.Admin.Controllers
{
    [Area("admin")]
    public class CountryController : Controller
    {
        private ICountryService _countryService;
        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        public IActionResult CreateCountry()
        {
            return View("CreateCountry", new Country());
        }
        [HttpPost]
        public IActionResult saveCountry(Country country)
        {
            _countryService.CreateCountry(country);
            return RedirectToAction("Table", "Table");
        }
        public IActionResult UpdateCountry(int id)
        {
            return View("UpdateCountry", _countryService.findbyId(id));

        }
        [HttpPost]
        public IActionResult EditCountry(Country country)
        {
            _countryService.UpdateCountry(country);
            return RedirectToAction("Table", "Table");
        }
        public IActionResult DeleteCountry(int id)
        {
            _countryService.DeleteCountry(id);
            return RedirectToAction("Table", "Table");
        }
    }
}
