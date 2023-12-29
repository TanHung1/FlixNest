using FlixNest.Areas.Identity.Data;
using FlixNest.Models;
using FlixNest.Repository.AccountRepository;
using FlixNest.Repository.MovieRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FlixNest.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Administrator")]
    public class HomeController : Controller
    {
        private readonly SignInManager<AccountUser> _signInManager;
        private IAccountRepository _accountRepository;
        private IMovieRepository _movieRepository;
        public HomeController(SignInManager<AccountUser> signInManager, IAccountRepository accountRepository,
                              IMovieRepository movieRepository)
        {
            _signInManager = signInManager;
            _accountRepository = accountRepository;
            _movieRepository = movieRepository;
        }
        public IActionResult Index()
        {
            int AccountCount = _accountRepository.CountAccount();
            ViewBag.CountAccount = AccountCount;
            int MovieCount = _movieRepository.MovieCount();
            ViewBag.MovieCount = MovieCount;
            List<Movie> movies = _movieRepository.GetMoviebyFollower().Take(1).ToList();
            ViewBag.MovieFollow = movies;
            List<Movie> MovieWithMostComment = _movieRepository.GetMovieWithMostComment().Take(1).ToList();
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
