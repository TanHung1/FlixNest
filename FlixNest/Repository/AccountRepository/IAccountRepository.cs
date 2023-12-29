using FlixNest.Areas.Identity.Data;

namespace FlixNest.Repository.AccountRepository
{
    public interface IAccountRepository
    {
        public List<AccountUser> GetAll();
        public bool CreateAccount(AccountUser accountUser);

        public bool UpdateAccount(AccountUser accountUser);

        public AccountUser GetAccount(string id);

        public bool DeleteAccount(string id);

        public int CountAccount();
    }

}
