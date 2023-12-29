using FlixNest.Models;

namespace FlixNest.Repository.MovieActorRepository
{
    public class MovieActorRepository : IMovieActorRepository
    {
        private FlixNestDbContext _context;
        public MovieActorRepository(FlixNestDbContext context)
        {
            _context = context;
        }
        public bool createMovieActor(Movie movie, List<int> ActorList)
        {
            foreach (var actorId in ActorList)
            {
                MovieActor movieactor = new MovieActor()
                {
                    ActId = actorId,
                    MovieId = movie.MovieId,
                    Actor = _context.Actor.Find(actorId),
                    Movie = movie
                };

                _context.MovieActor.Add(movieactor);
                _context.SaveChanges();
            }
            return true;
        }



        public bool updateMovieActor(Movie movie, List<int> ActorList)
        {
            //lấy danh sách hiện tại của phim
            var existactor = _context.MovieActor.Where(x => x.MovieId == movie.MovieId).ToList();
            //xóa những thứ không dc chọn
            foreach (var movieActor in existactor)
            {
                if (!ActorList.Contains(movieActor.ActId))
                {
                    _context.MovieActor.Remove(movieActor);
                }

            }
            foreach (var actorId in ActorList)
            {
                if (!existactor.Any(x => x.ActId == actorId))
                {
                    _context.MovieActor.Add(new MovieActor { ActId = actorId, MovieId = movie.MovieId });
                }
            }

            _context.SaveChanges();
            return true;
        }
    }
}
