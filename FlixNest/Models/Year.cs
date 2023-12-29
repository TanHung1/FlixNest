using System.ComponentModel.DataAnnotations;

namespace FlixNest.Models
{
    public class Year
    {
        [Key]
        public int YearId { get; set; }
        [Required(ErrorMessage = "Năm không được để trống")]
        public string YearName { get; set; } = null;

        public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
