using FlixNest.Models;
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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlixNest.Areas.Admin.Controllers
{
    [Area("admin")]
    public class MovieController : Controller
    {
        private FlixNestDbContext _context;
        private IMovieRepository _movieRepository;
        private IGenreRepository _genreRepository;
        private IActorRepository _actorRepository;
        private IDirectorRepository _DirectorRepository;
        private IMovieGenreRepository _movieGenreRepository;
        private IMovieActorRepository _movieActorRepository;
        private IMovieDirectorRepository _movieDirectorRepository;
        private ICountryRepository _countryRepository;
        private IYearRepository _yearRepository;
        private IEpisodeRepository _episodeRepository;
        public MovieController(FlixNestDbContext context, IMovieRepository movieRepository,
            IGenreRepository genreRepository, IMovieGenreRepository movieGenreRepository,
            IMovieActorRepository movieActorRepository, IMovieDirectorRepository movieDirectorRepository,
            IDirectorRepository directorRepository, IActorRepository actorRepository,
            IYearRepository yearRepository, ICountryRepository countryRepository,
            IEpisodeRepository episodeRepository)
        {
            _context = context;
            _movieRepository = movieRepository;
            _genreRepository = genreRepository;
            _actorRepository = actorRepository;
            _DirectorRepository = directorRepository;
            _movieGenreRepository = movieGenreRepository;
            _movieActorRepository = movieActorRepository;
            _movieDirectorRepository = movieDirectorRepository;
            _countryRepository = countryRepository;
            _yearRepository = yearRepository;
            _episodeRepository = episodeRepository;

        }
        public IActionResult Detail(int id)
        {
            Movie movie = _movieRepository.findbyId(id);
            List<Episode> episodes = _episodeRepository.GetEpisodeByMovieId(id);
            var movieGenres = _context.MovieGenre.Where(x => x.MovieId == id).Select(x => x.Genre).ToList();
            var actorGenres = _context.MovieActor.Where(x => x.MovieId == id).Select(x => x.Actor).ToList();
            var directorGenres = _context.MovieDirector.Where(x => x.MovieId == id).Select(x => x.Director).ToList();
            ViewBag.Genre = movieGenres;
            ViewBag.Actors = actorGenres;
            ViewBag.Directors = directorGenres;
            ViewBag.Episodes = episodes;
            ViewBag.Movie = movie;
            return View();
        }
        public IActionResult CreateMovie()
        {
            var q1 = _yearRepository.GetAll()
               .Select(q1 => new SelectListItem()
               {
                   Text = q1.YearName,
                   Value = q1.YearId.ToString(),
               }).ToList();
            ViewBag.Year = q1;
            var q2 = _countryRepository.GetAll()
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
                Movie currentVideo = _movieRepository.findbyId(movie.MovieId);
                movie.Image = currentVideo.Image;
            }
            var q1 = _yearRepository.GetAll()
                .Select(q1 => new SelectListItem()
                {
                    Text = q1.YearName,
                    Value = q1.YearId.ToString(),
                }).ToList();
            ViewBag.Year = q1;
            var q2 = _countryRepository.GetAll()
                .Select(q2 => new SelectListItem()
                {
                    Text = q2.CountryName,
                    Value = q2.CountryId.ToString(),
                }).ToList();


            ViewBag.Country = q2;
            _movieRepository.CreateMovie(movie, episode);
            _movieGenreRepository.createMovieGenre(movie, GenreList);
            _movieActorRepository.createMovieActor(movie, ActList);
            _movieDirectorRepository.createMovieDirector(movie, DirectorList);

            return RedirectToAction("Index", "Table");
        }
        public IActionResult UpdateMovie(int id)
        {
            Movie movie = _movieRepository.findbyId(id);

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

            var q1 = _yearRepository.GetAll()
               .Select(q1 => new SelectListItem()
               {
                   Text = q1.YearName,
                   Value = q1.YearId.ToString(),
               }).ToList();
            ViewBag.Year = q1;
            var q2 = _countryRepository.GetAll()
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
                Movie currentMovie = _movieRepository.findbyId(movie.MovieId);
                movie.Image = currentMovie.Image;
            }
            var q1 = _yearRepository.GetAll()
               .Select(q1 => new SelectListItem()
               {
                   Text = q1.YearName,
                   Value = q1.YearId.ToString(),
               }).ToList();
            ViewBag.Year = q1;
            var q2 = _countryRepository.GetAll()
                .Select(q2 => new SelectListItem()
                {
                    Text = q2.CountryName,
                    Value = q2.CountryId.ToString(),
                }).ToList();
            ViewBag.Country = q2;
            // Cập nhật thông tin phim
            _movieRepository.UpdateMovie(movie);
            _movieGenreRepository.updateMovieGenre(movie, GenreList);
            _movieActorRepository.updateMovieActor(movie, ActList);
            _movieDirectorRepository.updateMovieDirector(movie, DirectorList);
            return RedirectToAction("Index", "Table");
        }

        public IActionResult DeleteMoivie(int id)
        {
            _movieRepository.DeleteMovie(id);
            return RedirectToAction("Index", "Table");
        }

    }
}
