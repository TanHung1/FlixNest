using FlixNest.Models;

namespace FlixNest.Repository.MovieActorRepository
{
    public interface IMovieActorRepository
    {

        public bool createMovieActor(Movie movie, List<int> ActorList);

        public bool updateMovieActor(Movie movie, List<int> ActorList);
    }
}
