using System.Threading.Tasks;
using Shouldly;
using Xunit;
using MonitorV.Vehicles;
using MonitorV.Vehicles.Dto;
using Abp.Notifications;
using Abp;
using Moq;
using Castle.MicroKernel.Registration;
using Abp.Application.Services.Dto;

namespace MonitorV.Tests.Vehicles
{
    public class VehiclesAppServiceTests : MonitorVTestBase
    {
        private readonly IVehiclesAppService _vehicleAppService;
        private Mock<INotificationSubscriptionManager> notificationSubscriptionManager = new Mock<INotificationSubscriptionManager>();
        private Mock<INotificationPublisher> notificationPublisher = new Mock<INotificationPublisher>();

        public VehiclesAppServiceTests()
        {
            LocalIocManager.IocContainer.Register(
                Component.For<INotificationSubscriptionManager>().Instance(notificationSubscriptionManager.Object).IsDefault());

            LocalIocManager.IocContainer.Register(
                Component.For<INotificationPublisher>().Instance(notificationPublisher.Object).IsDefault());

            _vehicleAppService = Resolve<IVehiclesAppService>();
        }

        [Fact]
        public async Task GetAll_Default_Returns7Vehicles()
        {
            // Arrange
            var request = new VehiclesRequest { MaxResultCount = 20, SkipCount = 0 };            

            // Act
            var result = await _vehicleAppService.GetAll(request);

            // Assert
            result.Items.Count.ShouldBe(7);
            notificationSubscriptionManager.Verify(e => e.SubscribeAsync(It.IsAny<UserIdentifier>(), "VehicleStatusUpdate", null));
        }

        [Fact]
        public async Task GetAll_KalesCustomer_Returns3Vehicles()
        {
            // Arrange
            var request = new VehiclesRequest { MaxResultCount = 20, SkipCount = 0, Customer = "Kalles Grustransporter AB" };

            // Act
            var result = await _vehicleAppService.GetAll(request);


            // Assert
            result.Items.Count.ShouldBe(3);
            notificationSubscriptionManager.Verify(e => e.SubscribeAsync(It.IsAny<UserIdentifier>(), "VehicleStatusUpdate", null));
        }

        [Fact]
        public async Task UpdateStatus_Id_VehicleLastStatusUpdatedAndNotificationPublished()
        {
            // Arrange
            var id = "YS2R4X20005399401";
            var vehicle = await _vehicleAppService.Get(new EntityDto<string> { Id = id });
            var oldTime = vehicle.LastStatusUpdate;

            // Act
            await _vehicleAppService.UpdateStatus(id);

            // Assert
            vehicle = await _vehicleAppService.Get(new EntityDto<string> { Id = id });
            vehicle.LastStatusUpdate.ShouldNotBe(oldTime);
            notificationPublisher.Verify(e => e.PublishAsync("VehicleStatusUpdate", It.IsAny<NotificationData>(), null, NotificationSeverity.Info, null, null, null));
        }
    }
}
