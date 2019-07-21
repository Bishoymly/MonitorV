using Abp.Events.Bus;
using Abp.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorV.Vehicles
{
    [Serializable]
    public class VehicleStatusUpdateNotificationData : NotificationData
    {
        public string VehicleId { get; set; }
        public DateTime EventTime { get; set; }
    }
}
