using FlixNest.Areas.Identity.Data;
using FlixNest.Data;
using FlixNest.IAppServices;
using FlixNest.Models;
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
        private IMovieService _movieService;
        private IGenreService _genreService;
        private IActorService _actorService;
        private IDirectorService _DirectorService;
        private IMovieGenreService _movieGenreService;
        private IMovieActorService _movieActorService;
        private IMovieDirectorService _movieDirectorService;
        private IYearService _yearService;
        private ICountryService _countryService;
        private IEpisodeService _episodeService;
        private IAccountService _accountService;
        private readonly UserManager<AccountUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public TableController(FlixNestDbContext context, IMovieService movieService,
            IGenreService genreService, IMovieGenreService movieGenreService,
            IMovieActorService movieActorService, IMovieDirectorService movieDirectorService,
            IDirectorService directorService, IActorService actorService,
            ICountryService countryService, IYearService yearService,
            IEpisodeService episodeService, IAccountService accountService,
            UserManager<AccountUser> userManager, RoleManager<IdentityRole> roleManager,
            FlixNestContext flixNestContext)
        {
            _context = context;
            _movieService = movieService;
            _genreService = genreService;
            _actorService = actorService;
            _DirectorService = directorService;
            _movieGenreService = movieGenreService;
            _movieActorService = movieActorService;
            _movieDirectorService = movieDirectorService;
            _yearService = yearService;
            _countryService = countryService;
            _episodeService = episodeService;
            _accountService = accountService;
            _userManager = userManager;
            _roleManager = roleManager;
            _flixNestContext = flixNestContext;
        }
        public IActionResult Index()
        {
            List<Genre> genres = _genreService.GetAll();
            List<Actor> actors = _actorService.GetAll();
            List<Director> directors = _DirectorService.GetAll();
            List<Movie> movies = _movieService.Getallwith();
            Dictionary<int, string> genreName = _genreService.GetAllGenreNames();
            ViewBag.GenreName = genreName;
            Dictionary<int, string> actorName = _actorService.GetAllActors();
            ViewBag.ActorName = actorName;
            Dictionary<int, string> directorName = _DirectorService.GetAllDirector();
            ViewBag.DirectorName = directorName;
            return View(new { Movie = movies, Genre = genres, Actor = actors, Director = directors });
        }
        public IActionResult ListAccount()
        {
            var getAllRoles = _accountService.GetAllRoles();
            var userRolesViewModels = _accountService.GetAllUserRoles();
            ViewBag.Roles = getAllRoles;
            return View(userRolesViewModels);
        }
       
        public IActionResult Table()
        {
            List<Year> years = _yearService.GetAll();
            List<Country> countries = _countryService.GetAll();
            return View(new { Year = years, Country = countries });
        }
        public IActionResult MoviewithEp()
        {
            List<Movie> movies = _movieService.GetAll();
            List<Episode> episodes = _episodeService.GetAllEpisodes();
            return View(new { Movie = movies, Episode = episodes });
        }
    }
}
