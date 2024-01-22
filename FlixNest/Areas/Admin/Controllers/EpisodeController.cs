using FlixNest.IAppServices;
using FlixNest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlixNest.Areas.Admin.Controllers
{
    [Area("admin")]
    public class EpisodeController : Controller
    {
        private IEpisodeService _episodeService;
        private IMovieService _movieService;
        private FlixNestDbContext _context;
        public EpisodeController(IEpisodeService episodeService, IMovieService movieService, FlixNestDbContext context)
        {
            _episodeService = episodeService;
            _movieService = movieService;
            _context = context;
        }
        public IActionResult Index()
        {
            List<Movie> movies = _movieService.GetAll();
            List<Episode> episodes = _episodeService.GetAllEpisodes();
            return View(new { Movie = movies, Episode = episodes });
        }
        public IActionResult Detail(int id)
        {
            Movie movie = _movieService.findbyId(id);
            List<Episode> episodes = _episodeService.GetEpisodeByMovieId(id);
            var MovieGenres = _context.MovieGenre.Where(x => x.MovieId == id).Select(x => x.Genre).ToList();
            Episode firstep = episodes.FirstOrDefault();

            ViewBag.Movie = movie;
            ViewBag.Episodes = episodes;
            ViewBag.Genre = MovieGenres;
            ViewBag.firstepId = firstep?.EpisodeId;
            return View();
        }
        public IActionResult DetailVideo(int id)
        {
            Episode selectedEpisode = _episodeService.findById(id);

            Movie movie = _movieService.findbyId(selectedEpisode.MovieId);
            List<Episode> episodes = _episodeService.GetEpisodeByMovieId(selectedEpisode.MovieId);

            ViewBag.Movie = movie;
            ViewBag.Episodes = episodes;
            return View(selectedEpisode);
        }


        public IActionResult DeleteEp(int id, Episode episode)
        {
            _episodeService.DeleteEpisode(id);
            return RedirectToAction("MoviewithEp", "Table");
        }
        public IActionResult CreateEp()
        {
            var movies = _movieService.GetAll()
                .Select(x => new SelectListItem()
                {
                    Text = x.MovieName,
                    Value = x.MovieId.ToString(),
                }).ToList();
            ViewBag.MovieId = movies;
            return View("CreateEp", new Episode());
        }
        [HttpPost]
        public IActionResult saveEp(Episode episode,Movie movie, IFormFile videoFile)
        {
            if (videoFile != null && videoFile.Length > 0)
            {
                var videoPath = Path.Combine("wwwroot/videos", videoFile.FileName);
                using (var videoStream = new FileStream(videoPath, FileMode.Create))

                {
                    videoFile.CopyTo(videoStream);
                }
                episode.Video = videoFile.FileName;

            }
            else
            {
                Episode currentVideo = _episodeService.findById(episode.EpisodeId);
                episode.Video = currentVideo.Video;
            }
            var movies = _movieService.GetAll()
                .Select(x => new SelectListItem()
                {
                    Text = x.MovieName,
                    Value = x.MovieId.ToString(),
                }).ToList();
            ViewBag.MovieId = movies;
            _episodeService.CreateEpisode(episode, movie);
            return RedirectToAction("MoviewithEp", "Table");
        }
        public IActionResult UpdateEp(int id)
        {
            var movies = _movieService.GetAll()
                .Select(x => new SelectListItem()
                {
                    Text = x.MovieName,
                    Value = x.MovieId.ToString(),
                }).ToList();
            ViewBag.MovieId = movies;
            return View("UpdateEp", _episodeService.findById(id));
        }
        [HttpPost]
        public IActionResult EditEp(Episode episode,Movie movie, IFormFile videoFile)
        {
            if (videoFile != null && videoFile.Length > 0)
            {
                var videoPath = Path.Combine("wwwroot/videos", videoFile.FileName);
                using (var videoStream = new FileStream(videoPath, FileMode.Create))
                {
                    videoFile.CopyTo(videoStream);
                }
                episode.Video = videoFile.FileName;
            }
            else
            {
                Episode currentVideo = _episodeService.findById(episode.EpisodeId);
                episode.Video = currentVideo.Video;
            }
            var movies = _movieService.GetAll()
            .Select(x => new SelectListItem()
            {
                Text = x.MovieName,
                Value = x.MovieId.ToString(),
            }).ToList();
            ViewBag.MoviesId = movies;
            _episodeService.UpdateEpisode(episode, movie);
            return RedirectToAction("MoviewithEp", "Table");
        }

        public IActionResult EpisodeWaitingDelete()
        {
            List<Episode> episodes = _episodeService.getEpisodeDelete();
            return View(episodes);
        }
        public IActionResult DeleteCompleteEpisode(int id)
        {
            _episodeService.DeleteCompleteEpisode(id);
            return RedirectToAction("MoviewithEp", "Table");
        }
        public IActionResult RestoreEpisode(int id)
        {
            Episode episode = _episodeService.findById(id);
            if(episode != null)
            {
                _episodeService.RestoreEpisode(episode);
            }
            return RedirectToAction("MoviewithEp", "Table");
        }
    }
}
