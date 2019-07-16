using System.Threading.Tasks;
using Abp.Application.Services;
using MonitorV.Sessions.Dto;

namespace MonitorV.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
