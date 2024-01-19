using System.ComponentModel.DataAnnotations;

namespace FlixNest.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }
        [Required(ErrorMessage = "Tên phim không được để trống")]
        public string MovieName { get; set; } = null;

        public string MovieTitle { get; set; }

        public string MovieTime { get; set; }

        public int YearId { get; set; }

        public int CountryId { get; set; }

        public string Image { get; set; }
        public string Status { get; set; }
        public int FollowerCount { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsCreated { get; set; }
        public List<MovieGenre> movieGenres { get; set; }

        public List<MovieDirector> movieDirectors { get; set; }

        public List<MovieActor> movieActors { get; set; }


        public ICollection<Episode> episodes { get; set; }

        public virtual ICollection<MovieFollow> MovieFollows { get; set; }

        public virtual ICollection<MovieComment> MovieComments { get; set; }

        public virtual ICollection<MovieActivity> MovieActivities { get; set; }
    }
}
