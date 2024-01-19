using FlixNest.Areas.Identity.Data;
using FlixNest.Data;
using FlixNest.Migrations;
using FlixNest.Repository.AccountRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FlixNest.Areas.Admin.Controllers
{
    [Area("admin")]
    public class AccountController : Controller
    {
        private IAccountRepository _accountRepository;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AccountUser> _userManager;
        private readonly FlixNestContext _context;
        public AccountController(IAccountRepository accountRepository, RoleManager<IdentityRole> roleManager
            , UserManager<AccountUser> userManager, FlixNestContext context)
        {
            _accountRepository = accountRepository;
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        public IActionResult CreateAccount()
        {
            return View("CreateAccount", new AccountUser());
        }
        [HttpPost]
        public IActionResult saveAccount(AccountUser accountUser)
        {
            _accountRepository.CreateAccount(accountUser);
            return RedirectToAction("ListAccount", "Table");
        }

        public IActionResult UpdateAccount(string id)
        {
            var account = _accountRepository.GetAccount(id);
            ViewBag.Roles = _roleManager.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();

            return View(account);
        }
        [HttpPost]
        public IActionResult EditAccount(AccountUser accountUser, string roleName)
        {
            
           _accountRepository.UpdateAccount(accountUser, roleName);
            return RedirectToAction("ListAccount", "Table");
        }

        public IActionResult DeleteAccount(string id)
        {
            _accountRepository.DeleteAccount(id);
            return RedirectToAction("ListAccount", "Table");
        }

        //Phần Action tạo, sửa, xóa vai trò.
        public IActionResult CreateRole()
        {
            return View("CreateRole", new IdentityRole());
        }
        [HttpPost]
        public IActionResult SaveRole(IdentityRole role)
        {
            _accountRepository.CreateRole(role.Name);
            return RedirectToAction("ListAccount", "Table");
        }
        public IActionResult DeleteRole(string id)
        {
            _accountRepository.DeleteRole(id);
            return RedirectToAction("ListAccount", "Table");
        }
        [HttpPost]
        public IActionResult EditRole (IdentityRole role)
        {
            _accountRepository.UpdateRole(role);
            return RedirectToAction("ListAccount", "Table");
        }
        public IActionResult UpdateRole(string id)
        {
            var role = _accountRepository.GetRoleById(id);
            return View(role);
        }
    }
}
