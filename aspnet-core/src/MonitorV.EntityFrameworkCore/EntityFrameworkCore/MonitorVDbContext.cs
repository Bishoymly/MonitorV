using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using MonitorV.Authorization.Roles;
using MonitorV.Authorization.Users;
using MonitorV.MultiTenancy;
using MonitorV.Vehicles;

namespace MonitorV.EntityFrameworkCore
{
    public class MonitorVDbContext : AbpZeroDbContext<Tenant, Role, User, MonitorVDbContext>
    {
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<VehicleStatus> VehicleStatuses { get; set; }

        public MonitorVDbContext(DbContextOptions<MonitorVDbContext> options)
            : base(options)
        {
        }
    }
}
