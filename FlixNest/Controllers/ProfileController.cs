using FlixNest.Areas.Identity.Data;
using FlixNest.Models;
using FlixNest.Repository.AccountRepository;
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
        private IAccountRepository _accountRepository;
        public ProfileController(SignInManager<AccountUser> signManager, UserManager<AccountUser> userManager,
            IFollowRepository followRepository, FlixNestDbContext context, IAccountRepository accountRepository)
        {
            _signManager = signManager;
            _userManager = userManager;
            _followRepository = followRepository;
            _context = context;
            _accountRepository = accountRepository;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        public IActionResult UpdateProfile(AccountUser user, string rolename)
        {
            _accountRepository.UpdateAccount(user, rolename);
            return RedirectToAction("Index");
        }
        public IActionResult Following()
        {
            //Lấy UserId của tài khoản đang đăng nhập
            var userId = Guid.Parse(_userManager.GetUserId(User));
            //Lấy danh sách bộ phim mà tài khoản đã follow
            var movieFollowed = _context.MovieFollows.Where(x => x.UserId == userId).Select(x => x.MovieId).ToList();
            //Lấy danh thông tin movie từ movieId
            var Movie = _context.Movie.Where(x => movieFollowed.Contains(x.MovieId)).ToList();
            ViewBag.movies = Movie;
            return View();
        }
    }
}
