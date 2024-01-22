using FlixNest.IAppServices;
using FlixNest.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlixNest.Areas.Admin.Controllers
{
    [Area("admin")]
    public class GenreController : Controller
    {
        private IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        [HttpPost]
        public IActionResult saveGenre(Genre genre)
        {
            bool isGenreExist = _genreService.CheckNameGenre(genre.GenreName);
            if(isGenreExist)
            {
                ModelState.AddModelError(string.Empty, "Thể loại này đã có!");
                return View("CreateGenre");
            }
            _genreService.CreateGenre(genre);
                return RedirectToAction("Index", "Table");
            


        }
     
        public IActionResult CreateGenre()
        {
            return View("CreateGenre", new Genre());
        }
        [HttpPost]
        public IActionResult updateGenre(Genre genre)
        {
            _genreService.UpdateGenre(genre);
            return RedirectToAction("Index", "Table");
        }
        public IActionResult EditGenre(int id)
        {
            return View("EditGenre", _genreService.findbyId(id));
        }
        public IActionResult DeleteGenre(int id)
        {
            bool isGenreUsed = _genreService.CheckGenreUsed(id);
            if (isGenreUsed)
            {
                TempData["ErrorMessage"] = "Thể loại này đang được sử dụng trong một hoặc nhiều bộ phim.";
                return RedirectToAction("Index", "Table", new { area = "admin" });
            }

            _genreService.DeleteGenre(id);
            return RedirectToAction("Index", "Table", new { area = "admin" });
        }
    }
}
