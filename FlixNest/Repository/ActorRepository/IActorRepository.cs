using FlixNest.Models;

namespace FlixNest.Repository.ActorRepository
{
    public interface IActorRepository
    {
        public List<Actor> GetAll();

        Dictionary<int, string> GetAllActors();
        public bool CreateActor(Actor actor);
        public bool UpdateActor(Actor actor);
        public bool DeleteActor(int ActId);

        public Actor findActor(int ActId);

        public bool CheckNameActor(string firstname, string lastname);
    }
}
