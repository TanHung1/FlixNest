using System.ComponentModel.DataAnnotations;

namespace FlixNest.Models
{
    public class MovieActivity
    {
        [Key]
        public int ActivityId { get; set; }

        public string Description { get; set; }

        public int MovieId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
