using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MonitorV.Vehicles
{
    public class VehicleStatus : Entity<Guid>
    {
        [Required]
        public virtual Vehicle Vehicle { get; set; }

        [Required]
        public DateTime StatusUpdateTimeStamp { get; set; }
    }
}
