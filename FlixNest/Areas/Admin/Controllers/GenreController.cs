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
            bool isGenreExist = _genreRepository.CheckNameGenre(genre.GenreName);
            if(isGenreExist)
            {
                ModelState.AddModelError(string.Empty, "Thể loại này đã có!");
                return View("CreateGenre");
            }
                _genreRepository.CreateGenre(genre);
                return RedirectToAction("Index", "Table");
            


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
            bool isGenreUsed = _genreRepository.CheckGenreUsed(id);
            if (isGenreUsed)
            {
                TempData["ErrorMessage"] = "Thể loại này đang được sử dụng trong một hoặc nhiều bộ phim.";
                return RedirectToAction("Index", "Table", new { area = "admin" });
            }

            _genreRepository.DeleteGenre(id);
            return RedirectToAction("Index", "Table", new { area = "admin" });
        }
    }
}
