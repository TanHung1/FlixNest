using FlixNest.IAppServices;
using FlixNest.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlixNest.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ActorController : Controller
    {
        private IActorService _actorService;

        public ActorController(IActorService actorService)
        {
            _actorService = actorService;
        }
        [HttpPost]
        public IActionResult saveActor(Actor actor)
        {

            _actorService.CreateActor(actor);
            return RedirectToAction("Index", "Table");
        }
        public IActionResult CreateActor()
        {
            return View("CreateActor", new Actor());
        }

        public IActionResult DeleteActor(int id)
        {
            _actorService.DeleteActor(id);
            return RedirectToAction("Index", "Table");
        }
        [HttpPost]
        public IActionResult UpdateActor(Actor actor)
        {
            _actorService.UpdateActor(actor);
            return RedirectToAction("Index", "Table");
        }
        public IActionResult EditActor(int id)
        {
            return View("EditActor", _actorService.findActor(id));
        }
    }
}
