using System.ComponentModel.DataAnnotations;

namespace FlixNest.Models
{
    public class Director
    {
        [Key]
        public int DirId { get; set; }
        [Required(ErrorMessage = "Họ không được để trống")]
        public string Fname { get; set; } = null;
        [Required(ErrorMessage = "Tên không được để trống")]
        public string LName { get; set; } = null;

        public List<MovieDirector> movieDirectors { get; set; }
    }
}
