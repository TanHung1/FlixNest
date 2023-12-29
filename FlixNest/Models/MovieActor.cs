namespace FlixNest.Models
{

    public class MovieActor
    {

        public int MovieActorId { get; set; }
        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        public int ActId { get; set; }

        public Actor Actor { get; set; }
    }
}
