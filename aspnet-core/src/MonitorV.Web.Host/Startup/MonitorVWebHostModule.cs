using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MonitorV.Configuration;

namespace MonitorV.Web.Host.Startup
{
    [DependsOn(
       typeof(MonitorVWebCoreModule))]
    public class MonitorVWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public MonitorVWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MonitorVWebHostModule).GetAssembly());
        }
    }
}
