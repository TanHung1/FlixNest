using FlixNest.Areas.Identity.Data;
using FlixNest.Repository.AccountRepository;
using Microsoft.AspNetCore.Mvc;

namespace FlixNest.Areas.Admin.Controllers
{
    [Area("admin")]
    public class AccountController : Controller
    {
        private IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
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
            return View(account);
        }
        [HttpPost]
        public IActionResult EditAccount(AccountUser accountUser)
        {
            _accountRepository.UpdateAccount(accountUser);
            return RedirectToAction("ListAccount", "Table");
        }

        public IActionResult DeleteAccount(string id)
        {
            _accountRepository.DeleteAccount(id);
            return RedirectToAction("ListAccount", "Table");
        }

    }
}
