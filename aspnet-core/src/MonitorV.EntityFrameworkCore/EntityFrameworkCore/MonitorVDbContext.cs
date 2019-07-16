using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using MonitorV.Authorization.Roles;
using MonitorV.Authorization.Users;
using MonitorV.MultiTenancy;

namespace MonitorV.EntityFrameworkCore
{
    public class MonitorVDbContext : AbpZeroDbContext<Tenant, Role, User, MonitorVDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public MonitorVDbContext(DbContextOptions<MonitorVDbContext> options)
            : base(options)
        {
        }
    }
}
