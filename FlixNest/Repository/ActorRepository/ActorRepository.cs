using FlixNest.Models;
using Hangfire;

namespace FlixNest.Repository.ActorRepository
{
    public class ActorRepository : IActorRepository
    {
        private FlixNestDbContext _context;

        public ActorRepository(FlixNestDbContext context)
        {
            _context = context;
        }

        public bool CheckNameActor(string firstname, string lastname)
        {
            Actor actor = _context.Actor.Where(x => x.Fname.Trim() == firstname.Trim() && x.Lname.Trim() == lastname.Trim()).FirstOrDefault();
            if (actor != null)
            {
                return true;
            }
            return false;
        }

        public bool CreateActor(Actor actor)
        {
            _context.Actor.Add(actor);
            _context.SaveChanges();
            BackgroundJob.Enqueue(() => SuccessfulCreation(actor.ActId, actor.Fname, actor.Lname, "Tạo thành công"));

            return true;
        }

        public bool DeleteActor(int ActId)
        {
            Actor act = _context.Actor.FirstOrDefault(x => x.ActId == ActId);
            _context.Actor.Remove(act);
            _context.SaveChanges();
            BackgroundJob.Enqueue(() => SuccessfulDeleted(act.ActId, act.Fname, act.Lname, "Xóa thành công"));

            return true;
        }

        public Actor findActor(int ActId)
        {
            Actor act = _context.Actor.FirstOrDefault(x => x.ActId == ActId);
            return act;
        }

        public List<Actor> GetAll()
        {
            return _context.Actor.ToList();
        }

        public Dictionary<int, string> GetAllActors()
        {
            return _context.Actor.ToDictionary(x => x.ActId, x => x.Fname + " " + x.Lname);
        }

        public bool UpdateActor(Actor actor)
        {
            Actor act = _context.Actor.FirstOrDefault(x => x.ActId == actor.ActId);
            if (act != null)
            {
                act.Fname = actor.Fname;
                act.Lname = actor.Lname;

                _context.SaveChanges(true);
                BackgroundJob.Enqueue(() => SuccessfulUpdate(actor.ActId, actor.Fname, actor.Lname, "Cập nhật thành công"));

            }
            return true;
        }
        public void SuccessfulCreation(int id, string name, string lname, string des)
        {
            Actor createActor = _context.Actor.FirstOrDefault(x => x.ActId == id);
        }
        public void SuccessfulUpdate(int actorId, string fname, string lname, string des)
        {
            Actor updateActor = findActor(actorId);
        }
        public void SuccessfulDeleted(int actorId, string fname, string lname, string des)
        {
            Actor DeleteActor = findActor(actorId);
        }
    }
}
