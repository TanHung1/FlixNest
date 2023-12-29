using FlixNest.Areas.Identity.Data;
using FlixNest.Models;
using FlixNest.Repository.FollowRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FlixNest.Controllers
{
    public class ProfileController : Controller
    {
        private SignInManager<AccountUser> _signManager;
        private UserManager<AccountUser> _userManager;
        private IFollowRepository _followRepository;
        private FlixNestDbContext _context;
        public ProfileController(SignInManager<AccountUser> signManager, UserManager<AccountUser> userManager,
            IFollowRepository followRepository, FlixNestDbContext context)
        {
            _signManager = signManager;
            _userManager = userManager;
            _followRepository = followRepository;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Following()
        {
            //Lấy UserId của tài khoản đang đăng nhập
            var userId = Guid.Parse(_userManager.GetUserId(User));
            //Lấy danh sách bộ phim mà tài khoản đã follow
            var movieFollowed = _context.MovieFollows.Where(x => x.UserId == userId).Select(x => x.MovieId).ToList();
            //Lấy danh thông tin movie từ movieId
            var Movie = _context.Movie.Where(x => movieFollowed.Contains(x.MovieId)).ToList();
            return View(Movie);
        }
    }
}
