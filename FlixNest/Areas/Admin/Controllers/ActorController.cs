using FlixNest.Models;
using FlixNest.Repository.ActorRepository;
using Microsoft.AspNetCore.Mvc;

namespace FlixNest.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ActorController : Controller
    {
        private IActorRepository _actorRepository;

        public ActorController(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }
        [HttpPost]
        public IActionResult saveActor(Actor actor)
        {

            _actorRepository.CreateActor(actor);
            return RedirectToAction("Index", "Table");
        }
        public IActionResult CreateActor()
        {
            return View("CreateActor", new Actor());
        }

        public IActionResult DeleteActor(int id)
        {
            _actorRepository.DeleteActor(id);
            return RedirectToAction("Index", "Table");
        }
        [HttpPost]
        public IActionResult UpdateActor(Actor actor)
        {
            _actorRepository.UpdateActor(actor);
            return RedirectToAction("Index", "Table");
        }
        public IActionResult EditActor(int id)
        {
            return View("EditActor", _actorRepository.findActor(id));
        }
    }
}
