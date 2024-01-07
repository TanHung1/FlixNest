using FlixNest.Models;
using FlixNest.Repository.LogMovieRepository;
using Microsoft.AspNetCore.Mvc;

namespace FlixNest.Areas.Admin.Controllers
{
    [Area("admin")]
    public class LogMovieController : Controller
    {
        private ILogMovieRepository _logMovieRepository;
        public LogMovieController(ILogMovieRepository logMovieRepository)
        {
            _logMovieRepository = logMovieRepository;
        }
        public IActionResult Index()
        {
            List<MovieActivity> logMovies = _logMovieRepository.getAll();
            return View(logMovies);
        }
    }
}
