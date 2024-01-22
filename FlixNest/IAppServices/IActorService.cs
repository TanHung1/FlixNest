using FlixNest.Models;

namespace FlixNest.IAppServices
{
    public interface IActorService
    {
        public List<Actor> GetAll();

        Dictionary<int, string> GetAllActors();
        public void CreateActor(Actor actor);
        public void UpdateActor(Actor actor);
        public void DeleteActor(int ActId);

        public Actor findActor(int ActId);

        public bool CheckNameActor(string firstname, string lastname);
    }
}
