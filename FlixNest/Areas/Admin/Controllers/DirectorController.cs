using FlixNest.Models;
using FlixNest.Repository.DirectorRepository;
using Microsoft.AspNetCore.Mvc;

namespace FlixNest.Areas.Admin.Controllers
{
    [Area("admin")]
    public class DirectorController : Controller
    {
        private IDirectorRepository _directorRepository;
        public DirectorController(IDirectorRepository directorRepository)
        {
            _directorRepository = directorRepository;
        }

        [HttpPost]
        public IActionResult saveDir(Director director)
        {
            _directorRepository.CreateDirector(director);
            return RedirectToAction("Index", "Table");
        }
        public IActionResult CreateDir()
        {
            return View("CreateDir", new Director());
        }

        [HttpPost]
        public IActionResult updateDir(Director director)
        {
            _directorRepository.UpdateDirector(director);
            return RedirectToAction("Index", "Table");
        }

        public IActionResult EditDir(int id)
        {
            return View("EditDir", _directorRepository.findbyId(id));
        }

        public IActionResult DeleteDirector(int id)
        {
            _directorRepository.DeleteDirector(id);
            return RedirectToAction("Index", "Table");
        }
    }
}
