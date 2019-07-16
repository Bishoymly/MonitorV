using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using MonitorV.Configuration.Dto;

namespace MonitorV.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : MonitorVAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
