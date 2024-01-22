using FlixNest.IAppServices;
using FlixNest.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlixNest.Areas.Admin.Controllers
{
    [Area("admin")]
    public class YearController : Controller
    {
        private IYearService _yearService;
        public YearController(IYearService yearService)
        {
            _yearService = yearService;
        }

        public IActionResult CreateYear()
        {

            return View("CreateYear", new Year());
        }
        [HttpPost]
        public IActionResult saveYear(Year year)
        {
            bool isYearExist = _yearService.CheckYear(year.YearName);
            if(isYearExist)
            {
                ModelState.AddModelError(string.Empty, "Năm này đã có");
                return View("CreateYear");
            }
            _yearService.CreateYear(year);
            return RedirectToAction("Table", "Table");
        }
        public IActionResult UpdateYear(int id)
        {
            return View("UpdateYear", _yearService.findbyId(id));
        }
        [HttpPost]
        public IActionResult EditYear(Year year)
        {
            _yearService.UpdateYear(year);
            return RedirectToAction("Table", "Table");
        }
        public IActionResult DeleteYear(int id)
        {
            _yearService.DeleteYear(id);
            return RedirectToAction("Table", "Table");
        }
    }
}
