using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MonitorV.MultiTenancy.Dto;

namespace MonitorV.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

