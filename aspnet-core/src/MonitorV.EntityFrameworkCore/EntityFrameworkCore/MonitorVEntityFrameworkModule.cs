using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using MonitorV.EntityFrameworkCore.Seed;

namespace MonitorV.EntityFrameworkCore
{
    [DependsOn(
        typeof(MonitorVCoreModule), 
        typeof(AbpZeroCoreEntityFrameworkCoreModule))]
    public class MonitorVEntityFrameworkModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<MonitorVDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        MonitorVDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        MonitorVDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }
                });
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MonitorVEntityFrameworkModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            if (!SkipDbSeed)
            {
                var migrator = IocManager.Resolve<AbpZeroDbMigrator>();
                migrator.CreateOrMigrateForHost();
                SeedHelper.SeedHostDb(IocManager);
            }
        }
    }
}
