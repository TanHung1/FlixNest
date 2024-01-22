using FlixNest.Models;

namespace FlixNest.IAppServices
{
    public interface IMovieActorService
    {

        public void createMovieActor(Movie movie, List<int> ActorList);

        public void updateMovieActor(Movie movie, List<int> ActorList);
    }
}
