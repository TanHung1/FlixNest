using System.ComponentModel.DataAnnotations;

namespace FlixNest.Models
{
    public class Episode
    {
        [Key]
        public int EpisodeId { get; set; }

        public string EpisodeName { get; set; }

        public string Video { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int MovieId { get; set; }

        public Movie Movie { get; set; }
    }
}
