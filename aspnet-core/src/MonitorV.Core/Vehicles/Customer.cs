using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MonitorV.Vehicles
{
    public class Customer : Entity<Guid>
    {
        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        public virtual IList<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
