using FlixNest.Areas.Identity.Data;
using FlixNest.IAppServices;
using FlixNest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace FlixNest.Areas.Admin.Controllers
{
    [Area("admin")]
    public class MovieController : Controller
    {
        private FlixNestDbContext _context;
        private IMovieService _movieService;
        private IGenreService _genreService;
        private IActorService _actorService;
        private IDirectorService _DirectorService;
        private IMovieGenreService _movieGenreService;
        private IMovieActorService _movieActorService;
        private IMovieDirectorService _movieDirectorService;
        private ICountryService _countryService;
        private IYearService _yearService;
        private IEpisodeService _episodeService;
        private readonly UserManager<AccountUser> _userManager;
        private SignInManager<AccountUser> _signInManager;
        public MovieController(FlixNestDbContext context, IMovieService movieService,
            IGenreService genreService, IMovieGenreService movieGenreService,
            IMovieActorService movieActorService, IMovieDirectorService movieDirectorService,
            IDirectorService directorService, IActorService actorService,
            IYearService yearService, ICountryService countryService,
            IEpisodeService episodeService, UserManager<AccountUser> userManager,
            SignInManager<AccountUser> signInManager)
        {
            _context = context;
            _movieService = movieService;
            _genreService = genreService;
            _actorService = actorService;
            _DirectorService = directorService;
            _movieGenreService = movieGenreService;
            _movieActorService = movieActorService;
            _movieDirectorService = movieDirectorService;
            _countryService = countryService;
            _yearService = yearService;
            _episodeService = episodeService;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> LogMovie(int id)
        {
            Movie movie = _movieService.findbyId(id);
            var movieGenres = _context.MovieGenre.Where(x => x.MovieId == id).Select(x => x.Genre).ToList();
            List<MovieActivity> movieActivities = _movieService.GetMovieActivities(id);
            var userNames = new Dictionary<Guid, string>();
            foreach (var activity in movieActivities) 
            { 
                var User = await _userManager.FindByIdAsync(activity.UserId.ToString());
                userNames[activity.UserId] = User?.FullName ?? "không tồn tại";
            }
            ViewBag.UserNames = userNames;
            ViewBag.Movie = movie;
            ViewBag.Genre = movieGenres;
            ViewBag.LogMovie = movieActivities;
            return View();
        }
        public IActionResult Detail(int id)
        {
            Movie movie = _movieService.findbyId(id);
            List<Episode> episodes = _episodeService.GetEpisodeByMovieId(id);
            List<MovieActivity> movieActivities = _movieService.GetMovieActivities(id);
            var movieGenres = _context.MovieGenre.Where(x => x.MovieId == id).Select(x => x.Genre).ToList();
            var actorGenres = _context.MovieActor.Where(x => x.MovieId == id).Select(x => x.Actor).ToList();
            var directorGenres = _context.MovieDirector.Where(x => x.MovieId == id).Select(x => x.Director).ToList();
            ViewBag.Genre = movieGenres;
            ViewBag.Actors = actorGenres;
            ViewBag.Directors = directorGenres;
            ViewBag.Episodes = episodes;
            ViewBag.LogMovie = movieActivities;
            ViewBag.Movie = movie;
            return View();
        }       
        public IActionResult CreateMovie()
        {
            var q1 = _yearService.GetAll()
               .Select(q1 => new SelectListItem()
               {
                   Text = q1.YearName,
                   Value = q1.YearId.ToString(),
               }).ToList();
            ViewBag.Year = q1;
            var q2 = _countryService.GetAll()
                .Select(q2 => new SelectListItem()
                {
                    Text = q2.CountryName,
                    Value = q2.CountryId.ToString(),
                }).ToList();
            ViewBag.Country = q2;
            List<SelectListItem> selectListItems = _context.Genre.Select(a => new SelectListItem
            {
                Text = a.GenreName,
                Value = a.GenreId.ToString()
            }).ToList();
            ViewBag.GenreId = selectListItems;
            List<SelectListItem> selectactors = _context.Actor.Select(a => new SelectListItem
            {
                Text = $"{a.Fname} {a.Lname}",
                Value = a.ActId.ToString()
            }).ToList();
            ViewBag.ActorId = selectactors;
            List<SelectListItem> selectDirectors = _context.Director.Select(a => new SelectListItem
            {
                Text = $"{a.Fname} {a.LName}",
                Value = a.DirId.ToString()
            }).ToList();
            ViewBag.DirectorId = selectDirectors;
            return View("CreateMovie");
        }
        [HttpPost]
        public IActionResult saveMovie(Movie movie, Episode episode, IFormFile imageFIle,
            List<int> GenreList, List<int> ActList, List<int> DirectorList)
        {

            if (imageFIle != null && imageFIle.Length > 0)
            {
                var imagePath = Path.Combine("wwwroot/images", imageFIle.FileName);

                using (var imageStream = new FileStream(imagePath, FileMode.Create))
                {

                    imageFIle.CopyTo(imageStream);
                }

                movie.Image = imageFIle.FileName;

            }
            else
            {
                Movie currentVideo = _movieService.findbyId(movie.MovieId);
                movie.Image = currentVideo.Image;
            }
            var q1 = _yearService.GetAll()
                .Select(q1 => new SelectListItem()
                {
                    Text = q1.YearName,
                    Value = q1.YearId.ToString(),
                }).ToList();
            ViewBag.Year = q1;
            var q2 = _countryService.GetAll()
                .Select(q2 => new SelectListItem()
                {
                    Text = q2.CountryName,
                    Value = q2.CountryId.ToString(),
                }).ToList();


            ViewBag.Country = q2;
            _movieService.CreateMovie(movie, episode);
            _movieGenreService.createMovieGenre(movie, GenreList);
            _movieActorService.createMovieActor(movie, ActList);
            _movieDirectorService.createMovieDirector(movie, DirectorList);

            return RedirectToAction("Index", "Table");
        }
        public IActionResult UpdateMovie(int id)
        {
            Movie movie = _movieService.findbyId(id);

            // Load selected genres
            var movieGenres = _context.MovieGenre.Where(mg => mg.MovieId == id)
                .Select(mg => mg.GenreId).ToList();

            var movieActors = _context.MovieActor.Where(x => x.MovieId == id)
                .Select(x => x.ActId).ToList();

            var movieDirectors = _context.MovieDirector.Where(x => x.MovieId == id)
                .Select(x => x.DirId).ToList();

            var allGenres = _context.Genre.Select(a => new SelectListItem
            {
                Text = a.GenreName,
                Value = a.GenreId.ToString(),
                Selected = movieGenres.Contains(a.GenreId)
            }).ToList();

            ViewBag.GenreId = allGenres;

            var allactors = _context.Actor.Select(a => new SelectListItem
            {
                Text = $"{a.Fname} {a.Lname}",
                Value = a.ActId.ToString(),
                Selected = movieActors.Contains(a.ActId)
            }).ToList();
            ViewBag.ActorId = allactors;

            var alldirectors = _context.Director.Select(x => new SelectListItem
            {
                Text = $"{x.Fname}{x.LName}",
                Value = x.DirId.ToString(),
                Selected = movieDirectors.Contains(x.DirId)
            }).ToList();
            ViewBag.DirectorId = alldirectors;

            var q1 = _yearService.GetAll()
               .Select(q1 => new SelectListItem()
               {
                   Text = q1.YearName,
                   Value = q1.YearId.ToString(),
               }).ToList();
            ViewBag.Year = q1;
            var q2 = _countryService.GetAll()
                .Select(q2 => new SelectListItem()
                {
                    Text = q2.CountryName,
                    Value = q2.CountryId.ToString(),
                }).ToList();
            ViewBag.Country = q2;
            return View("UpdateMovie", movie);
        }

        [HttpPost]
        public IActionResult saveUpdateMovie(Movie movie, List<int> GenreList, IFormFile imageFile,
                                        List<int> ActList, List<int> DirectorList)
        {

            if (imageFile != null && imageFile.Length > 0)
            {
                var imagePath = Path.Combine("wwwroot/images", imageFile.FileName);
                using (var imageStream = new FileStream(imagePath, FileMode.Create))
                {
                    imageFile.CopyTo(imageStream);
                }
                movie.Image = imageFile.FileName;

            }
            else
            {
                Movie currentMovie = _movieService.findbyId(movie.MovieId);
                movie.Image = currentMovie.Image;
            }
            var q1 = _yearService.GetAll()
               .Select(q1 => new SelectListItem()
               {
                   Text = q1.YearName,
                   Value = q1.YearId.ToString(),
               }).ToList();
            ViewBag.Year = q1;
            var q2 = _countryService.GetAll()
                .Select(q2 => new SelectListItem()
                {
                    Text = q2.CountryName,
                    Value = q2.CountryId.ToString(),
                }).ToList();
            ViewBag.Country = q2;
            // Cập nhật thông tin phim
            _movieService.UpdateMovie(movie);
            _movieGenreService.updateMovieGenre(movie, GenreList);
            _movieActorService.updateMovieActor(movie, ActList);
            _movieDirectorService.updateMovieDirector(movie, DirectorList);
            return RedirectToAction("Index", "Table");
        }
        //// tao entity DataHistoryForReview{
        ////  id
        //    serializeData 
        //    status,
        //    lastModifyuser ,
        //isSameUser
        //// }
        public IActionResult DeleteMoivie(int id)
        {
            _movieService.DeleteMovie(id);
            
            return RedirectToAction("Index", "Table");
        }
        public IActionResult MovieWaitingDelete()
        {
            List<Movie> movies = _movieService.getMovieDelete();
            return View(movies);   
        }
        public IActionResult DeleteComple(int id)
        {
            _movieService.DeleteCompleteMovie(id);
            return RedirectToAction("Index", "Table");
        }

        public IActionResult ReStoreMovie(int id)
        {
            Movie movie = _movieService.findbyId(id); 
            if (movie != null)
            {
                _movieService.RestoreMovie(movie);
            }
            return RedirectToAction("Index", "Table");
        }
        public IActionResult ApproveMovie(int id)
        {
            Movie movie = _movieService.findbyId(id);
            if(movie != null)
            {
                _movieService.ApproveMovie(movie);
            }
            return RedirectToAction("Index", "Table");
        }
        public IActionResult RejectMovie(int id)
        {
            Movie movie = _movieService.findbyId(id);
            if( movie != null)
            {
                _movieService.RejectMovie(movie);
            }
            return RedirectToAction("Index", "Table");
        }
    }
}
