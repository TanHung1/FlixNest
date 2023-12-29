using System.ComponentModel.DataAnnotations;

namespace FlixNest.Models
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "Tên quốc gia không được để trống")]
        public string CountryName { get; set; } = null;

        public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
