using System.Threading.Tasks;
using MonitorV.Configuration.Dto;

namespace MonitorV.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
