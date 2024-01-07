using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlixNest.Models
{
    public class EpisodeActivity
    {
        [Key]
        public Guid EPActivityId { get; set; }

        public string Description { get; set; }

        public string Action { get; set; }

        public int EpisodeId { get; set; }
        [ForeignKey("EpisodeId")]
        public virtual Episode Episode { get; set; }

        public DateTime DateTime { get; set; }
    }
}
