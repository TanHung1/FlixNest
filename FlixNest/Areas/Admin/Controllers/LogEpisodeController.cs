using FlixNest.Models;
using FlixNest.Repository.LogEpisodeRepository;
using Microsoft.AspNetCore.Mvc;

namespace FlixNest.Areas.Admin.Controllers
{
    [Area("admin")]
    public class LogEpisodeController : Controller
    {
        private readonly ILogEpisodeRepository _logEpisodeRepository;
        public LogEpisodeController(ILogEpisodeRepository logEpisodeRepository)
        {
            _logEpisodeRepository = logEpisodeRepository;
        }

        public IActionResult Index()
        {
            List<EpisodeActivity> episodeActivities = _logEpisodeRepository.getALl();
            return View(episodeActivities);
        }
    }
}
