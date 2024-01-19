using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlixNest.Models
{
    public class MovieActivity
    {
        [Key]
        public Guid ActivityId { get; set; }

        public string Description { get; set; }
        public string Action { get; set; }
        public Guid UserId { get; set; }
        public int MovieId { get; set; }
        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }
        public DateTime DateTime { get; set; }
    }
}
