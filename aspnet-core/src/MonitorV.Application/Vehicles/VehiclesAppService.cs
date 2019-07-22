using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Localization;
using Abp.Notifications;
using Abp.RealTime;
using MonitorV.Vehicles.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorV.Vehicles
{
    public class VehiclesAppService : AsyncCrudAppService<Vehicle, VehicleDto, string, VehiclesRequest>, IVehiclesAppService
    {
        private IIocResolver _resolver;

        public INotificationSubscriptionManager SubscriptionManager { get; set; }
        public INotificationPublisher NotificationPublisher { get; set; }

        public VehiclesAppService(IRepository<Vehicle, string> repository, IIocResolver resolver) : base(repository)
        {
            _resolver = resolver;
        }

        protected override IQueryable<Vehicle> CreateFilteredQuery(VehiclesRequest input)
        {
            var query = Repository.GetAllIncluding(e => e.Customer);
            if (!string.IsNullOrEmpty(input.Customer))
                query = query.Where(e => e.Customer.Name == input.Customer);

            // Subscribe to real time updates for vehicle status
            if (AbpSession.UserId != null)
            {
                var user = new UserIdentifier(AbpSession.TenantId, AbpSession.UserId.Value);
                SubscriptionManager.SubscribeAsync(user, "VehicleStatusUpdate").Wait();
            }

            return query;
        }

        public async Task UpdateStatus(string id)
        {
            var vehicle = await GetEntityByIdAsync(id);

            var status = vehicle.UpdateStatus();

            var statusRepository = _resolver.Resolve<IRepository<VehicleStatus, Guid>>();
            statusRepository.Insert(status);

            // Update all users who subscribed to vehicle status notifications
            if (NotificationPublisher != null)
            {
                await NotificationPublisher.PublishAsync("VehicleStatusUpdate", new VehicleStatusUpdateNotificationData { VehicleId = id, EventTime = status.StatusUpdateTimeStamp });
            }
        }
    }
}
