using Abp.Domain.Entities;
using Abp.Events.Bus;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MonitorV.Vehicles
{
    public class Vehicle : Entity<string>
    {
        [Required]
        public string RegistrationNumber { get; set; }

        [Required]
        public virtual Customer Customer { get; set; }

        public DateTime? LastStatusUpdate { get; set; } = null;
        
        public VehicleStatus UpdateStatus()
        {
            var timestamp = DateTime.Now;
            this.LastStatusUpdate = timestamp;
            return new VehicleStatus { Id = Guid.NewGuid(), StatusUpdateTimeStamp = timestamp, Vehicle = this };
        }
    }
}
