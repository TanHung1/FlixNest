namespace FlixNest.Models
{
    public class MovieDirector
    {
        public int MovieDirectorId { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int DirId { get; set; }

        public Director Director { get; set; }
    }
}
