using FlixNest.Areas.Identity.Data;
using FlixNest.IAppServices;
using FlixNest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace FlixNest.Controllers
{
    public class HomeController : Controller
    {
        private IEpisodeService _episodeService;
        private IMovieService _movieService;
        private FlixNestDbContext _context;
        private SignInManager<AccountUser> _signInManager;
        private UserManager<AccountUser> _userManager;
        private IFollowService _followService;


        private IMovieCommentService _movieCommentService;
        public HomeController(IEpisodeService episodeService, IMovieService movieService, FlixNestDbContext context
            , SignInManager<AccountUser> signInManager, UserManager<AccountUser> userManager, IFollowService followService
            , IMovieCommentService movieCommentService)
        {
            _episodeService = episodeService;
            _movieService = movieService;
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _followService = followService;
            _movieCommentService = movieCommentService;

        }

        public IActionResult logout()
        {
            _signInManager.SignOutAsync();
            return LocalRedirect("/");
        }
        public IActionResult GetallMoviebyFollower()
        {
            List<Movie> movies = _movieService.GetMoviebyFollower();
            return View(movies);
        }
        public IActionResult GetallNewMovie(int? Page)
        {
            int pageNumber = Page ?? 1;
            int pageSize = 6;
            List<Episode> episodes = _episodeService.GetEpbyTime().ToList();
            List<Movie> allMovies = _movieService.GetAll();
            List<Movie> movieEpbyDate = episodes.GroupBy(x => x.MovieId)
                                                .Select(group => group.OrderByDescending(x => x.ReleaseDate).FirstOrDefault()?.Movie)
                                                .Where(movie => movie != null)
                                                .ToList();
            ViewBag.Movie = allMovies;
            ViewBag.movieEpTime = movieEpbyDate.ToPagedList(pageNumber, pageSize);
            return View();
        }
        public IActionResult Index()
        {
            List<Movie> movies = _movieService.GetMoviebyFollower().Take(3).ToList();
            List<Episode> episodes = _episodeService.GetEpbyTime().Take(6).ToList();
            List<Movie> movieEpbyDate = episodes.GroupBy(x => x.MovieId)
                                                .Select(group => group.OrderByDescending(x => x.ReleaseDate).First().Movie)
                                                .Where(movie => movie != null)
                                                .ToList();
            List<Movie> top6FollowedMovies = _movieService.GetMoviebyFollower().Take(6).ToList();
            ViewBag.MovieFollow = movies;
            ViewBag.movieEpTime = movieEpbyDate;
            ViewBag.Top6FollowedMovies = top6FollowedMovies;
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult Commnent(string userId, int MovieId, string title)
        {
            Movie movie = _movieService.findbyId(MovieId);
            _movieCommentService.UserComment(userId, MovieId, title);
            return RedirectToAction("Detail", new { id = MovieId });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Follow(int movieId, string userId)
        {
            Movie movie = _movieService.findbyId(movieId);
            _followService.FollowMovie(userId, movieId);
            return RedirectToAction("Detail", new { id = movieId });
        }
        [Authorize]
        public IActionResult UnFollow(int movieId)
        {
            Movie movie = _movieService.findbyId(movieId);
            // Get the current user's id
            var userId = _userManager.GetUserId(User);
            Guid userfollow = Guid.Parse(userId);
            // Find the MovieFollow entry for the current user and movie
            var movieFollow = _context.MovieFollows
                .FirstOrDefault(mf => mf.UserId == userfollow && mf.MovieId == movieId);

            if (movieFollow != null)
            {
                // Remove the MovieFollow entry
                _context.MovieFollows.Remove(movieFollow);
                if (movie != null && movie.FollowerCount > 0)
                {
                    movie.FollowerCount -= 1;
                }
                _context.SaveChanges();

                return RedirectToAction("Detail", new { id = movieId });
            }

            return NotFound();
        }


        public async Task<IActionResult> Detail(int id)
        {
            var movieCommnet = _context.MovieComments.Where(x => x.MovieId == id).ToList();
            Movie movie = _movieService.findbyId(id);
            List<Episode> episodes = _episodeService.GetEpisodeByMovieId(id);
            var MovieGenres = _context.MovieGenre.Where(x => x.MovieId == id).Select(x => x.Genre).ToList();
            Episode firstEp = episodes.FirstOrDefault();

            var userNames = new Dictionary<Guid, string>();
            foreach (var comment in movieCommnet)
            {
                var User = await _userManager.FindByIdAsync(comment.UserId.ToString());
                userNames[comment.UserId] = User?.FullName ?? "không tồn tại";
            }

            ViewBag.UserNames = userNames;
            ViewBag.Comment = movieCommnet;
            ViewBag.Movie = movie;
            ViewBag.Episodes = episodes;
            ViewBag.Genre = MovieGenres;
            ViewBag.FirstEp = firstEp?.EpisodeId;
            return View();
        }

        public IActionResult watching(int id)
        {
            Episode selectedEpisode = _episodeService.findById(id);

            Movie movie = _movieService.findbyId(selectedEpisode.MovieId);
            List<Episode> episodes = _episodeService.GetEpisodeByMovieId(selectedEpisode.MovieId);

            ViewBag.Movie = movie;
            ViewBag.Episodes = episodes;
            return View(selectedEpisode);

        }
        public IActionResult findMovieByYear(int id)
        {
            List<Movie> movies = _movieService.GetMoviebyYear(id);
            return View(movies);
        }
        public IActionResult findMovieByGenre(int genreId)
        {
            List<Movie> movies = _movieService.GetMovieByGenreName(genreId);
            return View(movies);
        }
        public IActionResult findbyMovieName(string name)
        {
            List<Movie> movies = _movieService.findMoviebyName(name);
            return View(movies);
        }
        public IActionResult findMovieByCountry(int id)
        {
            List<Movie> movies = _movieService.GetMoviebyCountry(id);
            return View(movies);
        }
    }
}