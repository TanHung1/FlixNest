using FlixNest.IAppServices;
using FlixNest.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlixNest.Areas.Admin.Controllers
{
    [Area("admin")]
    public class DirectorController : Controller
    {
        private IDirectorService _directorService;
        public DirectorController(IDirectorService directorService)
        {
            _directorService = directorService;
        }

        [HttpPost]
        public IActionResult saveDir(Director director)
        {
            _directorService.CreateDirector(director);
            return RedirectToAction("Index", "Table");
        }
        public IActionResult CreateDir()
        {
            return View("CreateDir", new Director());
        }

        [HttpPost]
        public IActionResult updateDir(Director director)
        {
            _directorService.UpdateDirector(director);
            return RedirectToAction("Index", "Table");
        }

        public IActionResult EditDir(int id)
        {
            return View("EditDir", _directorService.findbyId(id));
        }

        public IActionResult DeleteDirector(int id)
        {
            _directorService.DeleteDirector(id);
            return RedirectToAction("Index", "Table");
        }
    }
}
