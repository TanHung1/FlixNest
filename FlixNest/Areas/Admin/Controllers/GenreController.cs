using FlixNest.Models;
using FlixNest.Repository.GenreRepository;
using Microsoft.AspNetCore.Mvc;

namespace FlixNest.Areas.Admin.Controllers
{
    [Area("admin")]
    public class GenreController : Controller
    {
        private IGenreRepository _genreRepository;

        public GenreController(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }
        [HttpPost]
        public IActionResult saveGenre(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _genreRepository.CreateGenre(genre);
                return RedirectToAction("Index", "Table");
            }

            return View("CreateGenre");
        }

        public IActionResult CreateGenre()
        {
            return View("CreateGenre", new Genre());
        }
        [HttpPost]
        public IActionResult updateGenre(Genre genre)
        {
            _genreRepository.UpdateGenre(genre);
            return RedirectToAction("Index", "Table");
        }
        public IActionResult EditGenre(int id)
        {
            return View("EditGenre", _genreRepository.findbyId(id));
        }
        public IActionResult DeleteGenre(int id)
        {
            _genreRepository.DeleteGenre(id);
            return RedirectToAction("Index", "Table");
        }
    }
}
