using FlixNest.Areas.Identity.Data;
using FlixNest.Models;
using Microsoft.AspNetCore.Identity;

namespace FlixNest.Repository.MovieCommentRepository
{
    public class MovieCommentRepository : IMovieCommentRepository
    {
        private FlixNestDbContext _context;
        private UserManager<AccountUser> _userManager;
        public MovieCommentRepository(FlixNestDbContext context, UserManager<AccountUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public bool UserComment(string userId, int MovieId, string title)
        {
            Guid userComment = Guid.Parse(userId);
            var movieCommnet = new MovieComment
            {
                UserId = userComment,
                MovieId = MovieId,
                Title = title
            };
            _context.MovieComments.Add(movieCommnet);
            _context.SaveChanges();
            return true;

        }
    }
}
