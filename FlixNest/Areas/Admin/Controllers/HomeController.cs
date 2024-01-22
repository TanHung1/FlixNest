using FlixNest.Areas.Identity.Data;
using FlixNest.IAppServices;
using FlixNest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FlixNest.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Administrator, Staff")]
    public class HomeController : Controller
    {
        private readonly SignInManager<AccountUser> _signInManager;
        private IAccountService _accountService;
        private IMovieService _movieService;
        public HomeController(SignInManager<AccountUser> signInManager, IAccountService accountService,
                              IMovieService movieService)
        {
            _signInManager = signInManager;
            _accountService = accountService;
            _movieService = movieService;
        }
        public IActionResult Index()
        {
            int AccountCount = _accountService.CountAccount();
            ViewBag.CountAccount = AccountCount;
            int MovieCount = _movieService.MovieCount();
            ViewBag.MovieCount = MovieCount;
            List<Movie> movies = _movieService.GetMoviebyFollower().Take(1).ToList();
            ViewBag.MovieFollow = movies;
            List<Movie> MovieWithMostComment = _movieService.GetMovieWithMostComment().Take(1).ToList();
            ViewBag.MovieMostComment = MovieWithMostComment;
            return View();
        }

        public IActionResult logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
