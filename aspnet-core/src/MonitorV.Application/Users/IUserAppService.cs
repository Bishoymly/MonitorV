using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MonitorV.Roles.Dto;
using MonitorV.Users.Dto;

namespace MonitorV.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
