using FlixNest.Models;

namespace FlixNest.Repository.MovieActorRepository
{
    public interface IMovieActorRepository
    {

        public void createMovieActor(Movie movie, List<int> ActorList);

        public void updateMovieActor(Movie movie, List<int> ActorList);
    }
}
