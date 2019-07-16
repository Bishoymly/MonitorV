using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MonitorV.Configuration;
using MonitorV.Web;

namespace MonitorV.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class MonitorVDbContextFactory : IDesignTimeDbContextFactory<MonitorVDbContext>
    {
        public MonitorVDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MonitorVDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            MonitorVDbContextConfigurer.Configure(builder, configuration.GetConnectionString(MonitorVConsts.ConnectionStringName));

            return new MonitorVDbContext(builder.Options);
        }
    }
}
