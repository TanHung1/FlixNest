using FlixNest.Areas.Identity.Data;
using FlixNest.Data;
using FlixNest.IAppServices;
using FlixNest.Migrations.FlixNest;
using Microsoft.AspNetCore.Identity;

namespace FlixNest.AppServices
{
    public class AccountService : IAccountService
    {
        private readonly FlixNestContext _context;
        private SignInManager<AccountUser> _signInManager;
        private UserManager<AccountUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        public AccountService(FlixNestContext context, SignInManager<AccountUser> signInManager,
            UserManager<AccountUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public int CountAccount()
        {
            return _context.Users.Count();
        }

        public bool CreateAccount(AccountUser accountUser)
        {
            _context.Users.Add(accountUser);
            _context.SaveChanges();
            return true;
        }
        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return _roleManager.Roles.ToList();

        }
        public IdentityRole GetRoleById(string roleId)
        {
            return _roleManager.FindByIdAsync(roleId).Result;
        }
        public bool CreateRole(string roleName)
        {
            var roleExist = _roleManager.RoleExistsAsync(roleName).Result;
            if (!roleExist)
            {
                var result = _roleManager.CreateAsync(new IdentityRole(roleName)).Result;
                return result.Succeeded;
            }
            return false;
        }
        public bool UpdateRole(IdentityRole role)
        {
            var existingRole = _roleManager.FindByIdAsync(role.Id).Result;
            if (existingRole != null)
            {
                existingRole.Name = role.Name;

                var result = _roleManager.UpdateAsync(existingRole).Result;
                return result.Succeeded;
            }

            return false;
        }
        public bool DeleteRole(string roleId)
        {
            var role = _roleManager.FindByIdAsync(roleId).Result;
            if (role != null)
            {
                var result = _roleManager.DeleteAsync(role).Result;
                return result.Succeeded;
            }
            return false;

        }

        public bool DeleteAccount(string id)
        {
            AccountUser accountUser = _context.Users.FirstOrDefault(x => x.Id == id);
            _context.Remove(accountUser);
            _context.SaveChanges(true);
            return true;
        }

        public AccountUser GetAccount(string id)
        {
            AccountUser accountUser = _context.Users.FirstOrDefault(x => x.Id == id);
            return accountUser;
        }

        public List<(string UserId, string FullName, string Email, string PhoneNubmer, string RoleName)> GetAllUserRoles()
        {
            var userRoles = _context.UserRoles.ToList();
            var userRolesViewModels = new List<(string UserId, string FullName, string Email, string PhoneNubmer, string RoleName)>();
            foreach (var userRole in userRoles)
            {
                var user = _userManager.FindByIdAsync(userRole.UserId).Result;
                var role = _roleManager.FindByIdAsync(userRole.RoleId).Result;

                var userRolesViewModel = (user.Id, role.Name, user.FullName, user.Email, user.PhoneNumber);
                userRolesViewModels.Add(userRolesViewModel);
            }
            return userRolesViewModels;
        }

        public bool UpdateAccount(AccountUser accountUser, string rolename)
        {
            //lấy người dùng từ dữ liệu
            var existingUser = GetAccount(accountUser.Id);
            //Cập nhật thông tin người dùng
            if (existingUser != null)
            {
                existingUser.FullName = accountUser.FullName;
                existingUser.Email = accountUser.Email;
                existingUser.PhoneNumber = accountUser.PhoneNumber;
            }
            //Cập nhật Vai trò người dùng
            var userRoles = _userManager.GetRolesAsync(existingUser).Result;
            var resultRemoveRoles = _userManager.RemoveFromRolesAsync(existingUser, userRoles).Result;
            if (resultRemoveRoles.Succeeded)
            {
                var resultAddRole = _userManager.AddToRoleAsync(existingUser, rolename).Result;
                if (resultAddRole.Succeeded)
                {
                    _context.SaveChanges();
                }
            }
            return true;
        }
    }
}
