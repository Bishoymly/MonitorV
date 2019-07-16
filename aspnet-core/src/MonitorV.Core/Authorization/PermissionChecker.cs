using Abp.Authorization;
using MonitorV.Authorization.Roles;
using MonitorV.Authorization.Users;

namespace MonitorV.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
