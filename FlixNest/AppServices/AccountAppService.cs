//using FlixNest.Areas.Identity.Data;
//using FlixNest.Data;
//using FlixNest.Dtos;
//using FlixNest.Models;
//using Microsoft.AspNetCore.Identity;

//namespace FlixNest.Repository.AccountRepository
//{
//    public class AccountAppService : IAccountRepository
//    {
//        private readonly FlixNestContext _context;
//        private SignInManager<AccountUser> _signInManager;
//        private UserManager<AccountUser> _userManager;

//        public AccountAppService(FlixNestContext context, SignInManager<AccountUser> signInManager, UserManager<AccountUser> userManager)
//        {
//            _context = context;
//            _signInManager = signInManager;
//            _userManager = userManager;
//        }

//        public int CountAccount()
//        {
//            return _context.Users.Count();
//        }

//        public async Task CreateAccount(AccountUser accountUser)
//        {

//            var UserDto = new CreateUserDto
//            {
//                username = input.UserName;
//            password = input.password;
//        };

//        var newUser = ObjectMapper.Map<UserDto, Actor>();
//        var idNewUser = _actorRepo.InsertAdnGetByidAsy(newUser);
//        //Table LogsUser
//        LogsUser(idNewUser, "user created ");
//    }
//    public async task LogsUser(int id, string Description)
//    {

//    }

//    public bool DeleteAccount(string id)
//    {
//        AccountUser accountUser = (AccountUser)_context.Users.FirstOrDefault(x => x.Id == id);
//        _context.Remove(accountUser);
//        _context.SaveChanges(true);
//        return true;
//    }

//    public AccountUser GetAccount(string id)
//    {
//        AccountUser accountUser = (AccountUser)_context.Users.FirstOrDefault(x => x.Id == id);
//        return accountUser;
//    }

//    public List<AccountUser> GetAll()
//    {
//        return _context.Users.ToList();
//    }

//    public bool UpdateAccount(AccountUser accountUser)
//    {
//        AccountUser accountUser1 = _context.Users.FirstOrDefault(x => x.Id == accountUser.Id);
//        if (accountUser1 != null)
//        {
//            accountUser1.FullName = accountUser.FullName;
//            accountUser1.Email = accountUser.Email;
//            _context.SaveChanges();
//        }
//        return true;
//    }
//}
//}
