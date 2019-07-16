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
    public class Vehicle : Entity<string>, IAggregateRoot<string>
    {
        [Required]
        public string RegistrationNumber { get; set; }

        [Required]
        public virtual Customer Customer { get; set; }

        public DateTime? LastStatusUpdate { get; set; } = null;
        
        [NotMapped]
        public ICollection<IEventData> DomainEvents { get; set; } = new Collection<IEventData>();
    }
}
