using System.ComponentModel.DataAnnotations;

namespace FlixNest.Models
{
    public class MovieComment
    {
        [Key]
        public int MovieCommentId { get; set; }

        public Guid UserId { get; set; }

        public int MovieId { get; set; }
        public string Title { get; set; }

    }
}
