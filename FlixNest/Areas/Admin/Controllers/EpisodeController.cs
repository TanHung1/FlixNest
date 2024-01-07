using FlixNest.Models;
using FlixNest.Repository.EpisodeRepository;
using FlixNest.Repository.MovieRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlixNest.Areas.Admin.Controllers
{
    [Area("admin")]
    public class EpisodeController : Controller
    {
        private IEpisodeRepository _episodeRepository;
        private IMovieRepository _movieRepository;
        private FlixNestDbContext _context;
        public EpisodeController(IEpisodeRepository episodeRepository, IMovieRepository movieRepository, FlixNestDbContext context)
        {
            _episodeRepository = episodeRepository;
            _movieRepository = movieRepository;
            _context = context;
        }
        public IActionResult Index()
        {
            List<Movie> movies = _movieRepository.GetAll();
            List<Episode> episodes = _episodeRepository.GetAllEpisodes();
            return View(new { Movie = movies, Episode = episodes });
        }
        public IActionResult Detail(int id)
        {
            Movie movie = _movieRepository.findbyId(id);
            List<Episode> episodes = _episodeRepository.GetEpisodeByMovieId(id);
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
            Episode selectedEpisode = _episodeRepository.findById(id);

            Movie movie = _movieRepository.findbyId(selectedEpisode.MovieId);
            List<Episode> episodes = _episodeRepository.GetEpisodeByMovieId(selectedEpisode.MovieId);

            ViewBag.Movie = movie;
            ViewBag.Episodes = episodes;
            return View(selectedEpisode);
        }


        public IActionResult DeleteEp(int id, Episode episode)
        {
            _episodeRepository.DeleteEpisode(id);
            return RedirectToAction("MoviewithEp", "Table");
        }
        public IActionResult CreateEp()
        {
            var movies = _movieRepository.GetAll()
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
                Episode currentVideo = _episodeRepository.findById(episode.EpisodeId);
                episode.Video = currentVideo.Video;
            }
            var movies = _movieRepository.GetAll()
                .Select(x => new SelectListItem()
                {
                    Text = x.MovieName,
                    Value = x.MovieId.ToString(),
                }).ToList();
            ViewBag.MovieId = movies;
            _episodeRepository.CreateEpisode(episode, movie);
            return RedirectToAction("MoviewithEp", "Table");
        }
        public IActionResult UpdateEp(int id)
        {
            var movies = _movieRepository.GetAll()
                .Select(x => new SelectListItem()
                {
                    Text = x.MovieName,
                    Value = x.MovieId.ToString(),
                }).ToList();
            ViewBag.MovieId = movies;
            return View("UpdateEp", _episodeRepository.findById(id));
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
                Episode currentVideo = _episodeRepository.findById(episode.EpisodeId);
                episode.Video = currentVideo.Video;
            }
            var movies = _movieRepository.GetAll()
            .Select(x => new SelectListItem()
            {
                Text = x.MovieName,
                Value = x.MovieId.ToString(),
            }).ToList();
            ViewBag.MoviesId = movies;
            _episodeRepository.UpdateEpisode(episode, movie);
            return RedirectToAction("MoviewithEp", "Table");
        }

        public IActionResult EpisodeWaitingDelete()
        {
            List<Episode> episodes = _episodeRepository.getEpisodeDelete();
            return View(episodes);
        }
        public IActionResult DeleteCompleteEpisode(int id)
        {
            _episodeRepository.DeleteCompleteEpisode(id);
            return RedirectToAction("MoviewithEp", "Table");
        }
        public IActionResult RestoreEpisode(int id)
        {
            Episode episode = _episodeRepository.findById(id);
            if(episode != null)
            {
                _episodeRepository.RestoreEpisode(episode);
            }
            return RedirectToAction("MoviewithEp", "Table");
        }
    }
}
