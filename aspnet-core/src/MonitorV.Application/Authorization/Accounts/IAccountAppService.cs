using System.Threading.Tasks;
using Abp.Application.Services;
using MonitorV.Authorization.Accounts.Dto;

namespace MonitorV.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
