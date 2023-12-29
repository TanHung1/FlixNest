using FlixNest.Models;
using FlixNest.Repository.YearRepository;
using Microsoft.AspNetCore.Mvc;

namespace FlixNest.Areas.Admin.Controllers
{
    [Area("admin")]
    public class YearController : Controller
    {
        private IYearRepository _yearRepository;
        public YearController(IYearRepository yearRepository)
        {
            _yearRepository = yearRepository;
        }

        public IActionResult CreateYear()
        {

            return View("CreateYear", new Year());
        }
        [HttpPost]
        public IActionResult saveYear(Year year)
        {
            _yearRepository.CreateYear(year);
            return RedirectToAction("Table", "Table");
        }
        public IActionResult UpdateYear(int id)
        {
            return View("UpdateYear", _yearRepository.findbyId(id));
        }
        [HttpPost]
        public IActionResult EditYear(Year year)
        {
            _yearRepository.UpdateYear(year);
            return RedirectToAction("Table", "Table");
        }
        public IActionResult DeleteYear(int id)
        {
            _yearRepository.DeleteYear(id);
            return RedirectToAction("Table", "Table");
        }
    }
}
