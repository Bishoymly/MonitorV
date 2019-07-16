using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MonitorV.Authorization;

namespace MonitorV
{
    [DependsOn(
        typeof(MonitorVCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class MonitorVApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<MonitorVAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(MonitorVApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
