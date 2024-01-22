using FlixNest.Areas.Identity.Data;
using FlixNest.Data;
using FlixNest.IAppServices;
using FlixNest.Migrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FlixNest.Areas.Admin.Controllers
{
    [Area("admin")]
    public class AccountController : Controller
    {
        private IAccountService _accountService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AccountUser> _userManager;
        private readonly FlixNestContext _context;
        public AccountController(IAccountService accountService, RoleManager<IdentityRole> roleManager
            , UserManager<AccountUser> userManager, FlixNestContext context)
        {
            _accountService = accountService;
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
            _accountService.CreateAccount(accountUser);
            return RedirectToAction("ListAccount", "Table");
        }

        public IActionResult UpdateAccount(string id)
        {
            var account = _accountService.GetAccount(id);
            ViewBag.Roles = _roleManager.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();

            return View(account);
        }
        [HttpPost]
        public IActionResult EditAccount(AccountUser accountUser, string roleName)
        {

            _accountService.UpdateAccount(accountUser, roleName);
            return RedirectToAction("ListAccount", "Table");
        }

        public IActionResult DeleteAccount(string id)
        {
            _accountService.DeleteAccount(id);
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
            _accountService.CreateRole(role.Name);
            return RedirectToAction("ListAccount", "Table");
        }
        public IActionResult DeleteRole(string id)
        {
            _accountService.DeleteRole(id);
            return RedirectToAction("ListAccount", "Table");
        }
        [HttpPost]
        public IActionResult EditRole (IdentityRole role)
        {
            _accountService.UpdateRole(role);
            return RedirectToAction("ListAccount", "Table");
        }
        public IActionResult UpdateRole(string id)
        {
            var role = _accountService.GetRoleById(id);
            return View(role);
        }
    }
}
