using FlixNest.Areas.Identity.Data;
using FlixNest.Data;
using FlixNest.Models;
using FlixNest.Repository.AccountRepository;
using FlixNest.Repository.ActorRepository;
using FlixNest.Repository.CountryRepository;
using FlixNest.Repository.DirectorRepository;
using FlixNest.Repository.EpisodeRepository;
using FlixNest.Repository.GenreRepository;
using FlixNest.Repository.MovieActorRepository;
using FlixNest.Repository.MovieDirectorRepository;
using FlixNest.Repository.MovieGenreRepository;
using FlixNest.Repository.MovieRepository;
using FlixNest.Repository.YearRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
namespace FlixNest.Areas.Admin.Controllers
{
    [Area("admin")]
    public class TableController : Controller
    {
        private FlixNestDbContext _context;
        private readonly FlixNestContext _flixNestContext;
        private IMovieRepository _movieRepository;
        private IGenreRepository _genreRepository;
        private IActorRepository _actorRepository;
        private IDirectorRepository _DirectorRepository;
        private IMovieGenreRepository _movieGenreRepository;
        private IMovieActorRepository _movieActorRepository;
        private IMovieDirectorRepository _movieDirectorRepository;
        private IYearRepository _yearRepository;
        private ICountryRepository _countryRepository;
        private IEpisodeRepository _episodeRepository;
        private IAccountRepository _accountRepository;
        private readonly UserManager<AccountUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public TableController(FlixNestDbContext context, IMovieRepository movieRepository,
            IGenreRepository genreRepository, IMovieGenreRepository movieGenreRepository,
            IMovieActorRepository movieActorRepository, IMovieDirectorRepository movieDirectorRepository,
            IDirectorRepository directorRepository, IActorRepository actorRepository,
            ICountryRepository countryRepository, IYearRepository yearRepository,
            IEpisodeRepository episodeRepository, IAccountRepository accountRepository,
            UserManager<AccountUser> userManager, RoleManager<IdentityRole> roleManager,
            FlixNestContext flixNestContext)
        {
            _context = context;
            _movieRepository = movieRepository;
            _genreRepository = genreRepository;
            _actorRepository = actorRepository;
            _DirectorRepository = directorRepository;
            _movieGenreRepository = movieGenreRepository;
            _movieActorRepository = movieActorRepository;
            _movieDirectorRepository = movieDirectorRepository;
            _yearRepository = yearRepository;
            _countryRepository = countryRepository;
            _episodeRepository = episodeRepository;
            _accountRepository = accountRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _flixNestContext = flixNestContext;
        }
        public IActionResult Index()
        {
            List<Genre> genres = _genreRepository.GetAll();
            List<Actor> actors = _actorRepository.GetAll();
            List<Director> directors = _DirectorRepository.GetAll();
            List<Movie> movies = _movieRepository.Getallwith();
            Dictionary<int, string> genreName = _genreRepository.GetAllGenreNames();
            ViewBag.GenreName = genreName;
            Dictionary<int, string> actorName = _actorRepository.GetAllActors();
            ViewBag.ActorName = actorName;
            Dictionary<int, string> directorName = _DirectorRepository.GetAllDirector();
            ViewBag.DirectorName = directorName;
            return View(new { Movie = movies, Genre = genres, Actor = actors, Director = directors });
        }
        public IActionResult ListAccount()
        {
            var getAllRoles = _accountRepository.GetAllRoles();
            var userRolesViewModels = _accountRepository.GetAllUserRoles();
            ViewBag.Roles = getAllRoles;
            return View(userRolesViewModels);
        }
       
        public IActionResult Table()
        {
            List<Year> years = _yearRepository.GetAll();
            List<Country> countries = _countryRepository.GetAll();
            return View(new { Year = years, Country = countries });
        }
        public IActionResult MoviewithEp()
        {
            List<Movie> movies = _movieRepository.GetAll();
            List<Episode> episodes = _episodeRepository.GetAllEpisodes();
            return View(new { Movie = movies, Episode = episodes });
        }
    }
}
