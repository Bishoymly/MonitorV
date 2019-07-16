using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace MonitorV.EntityFrameworkCore
{
    public static class MonitorVDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<MonitorVDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<MonitorVDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
