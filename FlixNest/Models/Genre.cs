using System.ComponentModel.DataAnnotations;

namespace FlixNest.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }

        [Required(ErrorMessage = "Tên thể loại không được để trống")]
        [StringLength(50, ErrorMessage = "Tên không dược quá 50 ký tự")]
        public string GenreName { get; set; } = null;

        public List<MovieGenre> movieGenres { get; set; }
    }
}
