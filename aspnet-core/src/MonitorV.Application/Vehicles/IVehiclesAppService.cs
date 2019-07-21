using Abp.Application.Services;
using MonitorV.Vehicles.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorV.Vehicles
{
    public interface IVehiclesAppService : IAsyncCrudAppService<VehicleDto, string, VehiclesRequest>
    {
    }
}
