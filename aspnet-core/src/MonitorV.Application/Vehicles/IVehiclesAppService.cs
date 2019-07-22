using Abp.Application.Services;
using MonitorV.Vehicles.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MonitorV.Vehicles
{
    public interface IVehiclesAppService : IAsyncCrudAppService<VehicleDto, string, VehiclesRequest>
    {
        Task UpdateStatus(string id);
    }
}
