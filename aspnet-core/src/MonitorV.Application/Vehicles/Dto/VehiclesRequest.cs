using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorV.Vehicles.Dto
{
    public class VehiclesRequest : PagedAndSortedResultRequestDto
    {
        public string Customer { get; set; }
    }
}
