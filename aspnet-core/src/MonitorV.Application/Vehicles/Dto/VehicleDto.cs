using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorV.Vehicles.Dto
{
    [AutoMap(typeof(Vehicle))]
    public class VehicleDto : EntityDto<string>
    {
        public string RegistrationNumber { get; set; }

        public string CustomerName { get; set; }

        public string CustomerAddress { get; set; }

        public DateTime? LastStatusUpdate { get; set; } = null;
    }
}
