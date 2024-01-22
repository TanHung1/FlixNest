using FlixNest.IAppServices;
using FlixNest.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlixNest.Areas.Admin.Controllers
{
    [Area("admin")]
    public class LogEpisodeController : Controller
    {
        private readonly ILogEpisodeService _logEpisodeService;
        public LogEpisodeController(ILogEpisodeService logEpisodeService)
        {
            _logEpisodeService = logEpisodeService;
        }

        public IActionResult Index()
        {
            List<EpisodeActivity> episodeActivities = _logEpisodeService.getALl();
            return View(episodeActivities);
        }
    }
}
