using FlixNest.IAppServices;
using FlixNest.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlixNest.Areas.Admin.Controllers
{
    [Area("admin")]
    public class LogMovieController : Controller
    {
        private ILogMovieService _logMovieService;
        public LogMovieController(ILogMovieService logMovieService)
        {
            _logMovieService = logMovieService;
        }
        public IActionResult Index()
        {
            List<MovieActivity> logMovies = _logMovieService.getAll();
            return View(logMovies);
        }
    }
}
