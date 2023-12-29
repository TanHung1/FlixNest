using System.ComponentModel.DataAnnotations;

namespace FlixNest.Models
{
    public class MovieFollow
    {
        [Key]
        public int MovieFollowId { get; set; }
        public Guid UserId { get; set; }
        public int MovieId { get; set; }



    }
}
