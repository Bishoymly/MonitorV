using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace MonitorV.Controllers
{
    public abstract class MonitorVControllerBase: AbpController
    {
        protected MonitorVControllerBase()
        {
            LocalizationSourceName = MonitorVConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
