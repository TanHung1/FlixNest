using FlixNest.Areas.Identity.Data;
using FlixNest.Data;
using FlixNest.Models;
using Microsoft.AspNetCore.Identity;

namespace FlixNest.Repository.FollowRepository
{
    public class FollowRepository : IFollowRepository
    {
        private FlixNestDbContext _context;
        private FlixNestContext _contextFactory;
        private UserManager<AccountUser> _userManager;
        private SignInManager<AccountUser> _signInManager;
        public FollowRepository(FlixNestDbContext context, FlixNestContext flixNest,
            UserManager<AccountUser> userManager, SignInManager<AccountUser> signInManager)
        {
            _context = context;
            _contextFactory = flixNest;
            _userManager = userManager;
            _signInManager = signInManager;
        }



        public bool FollowMovie(string userId, int Movieid)
        {
            Guid userFollow = Guid.Parse(userId);
            if (!IsFollowingMovie(userId, Movieid))
            {
                var followEntry = new MovieFollow
                {
                    UserId = userFollow,
                    MovieId = Movieid
                };


                _context.MovieFollows.Add(followEntry);
                var movie = _context.Movie.Find(Movieid);
                if (movie != null)
                {
                    movie.FollowerCount += 1;
                }
                _context.SaveChanges();
                return true;

            }
            return false;
        }

        public bool IsFollowingMovie(string userId, int movieId)
        {
            Guid userfollow = Guid.Parse(userId);
            return _context.MovieFollows.Any(f => f.UserId == userfollow && f.MovieId == movieId);

        }



    }
}
