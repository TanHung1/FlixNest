using System.ComponentModel.DataAnnotations;

namespace FlixNest.Models
{
    public class Actor
    {
        [Key]
        public int ActId { get; set; }

        [Required(ErrorMessage = "Họ không được để trống")]
        public string Fname { get; set; } = null;

        [Required(ErrorMessage = "Tên không được để trống")]
        public string Lname { get; set; } = null;

        public List<MovieActor> movieActors { get; set; }
    }
}
