using FlixNest.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace FlixNest.IAppServices
{
    public interface IAccountService
    {
        public List<(string UserId, string FullName, string Email, string PhoneNubmer, string RoleName)> GetAllUserRoles();

        public IEnumerable<IdentityRole> GetAllRoles();
        public bool CreateAccount(AccountUser accountUser);

        public IdentityRole GetRoleById(string roleId);
        public bool CreateRole(string roleName);

        public bool UpdateRole(IdentityRole role);
        public bool DeleteRole(string roleId);

        public bool UpdateAccount(AccountUser accountUser, string rolename);

        public AccountUser GetAccount(string id);

        public bool DeleteAccount(string id);

        public int CountAccount();
    }

}
