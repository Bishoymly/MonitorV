using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Abp.Localization;
using MonitorV.Vehicles;
using System;

namespace MonitorV.EntityFrameworkCore.Seed.Host
{
    public class DefaultVehiclesCreator
    {
        public static List<Customer> InitialCustomers => GetInitialCustomers();

        private readonly MonitorVDbContext _context;

        private static List<Customer> GetInitialCustomers()
        {
            return new List<Customer>
            {
                new Customer{Id = Guid.NewGuid(), Name = "Kalles Grustransporter AB", Address = "Cementvägen 8, 111 11 Södertälje", Vehicles = new List<Vehicle>{
                    new Vehicle{Id = "YS2R4X20005399401", RegistrationNumber = "ABC123" },
                    new Vehicle{Id = "VLUR4X20009093588", RegistrationNumber = "DEF456" },
                    new Vehicle{Id = "VLUR4X20009048066", RegistrationNumber = "GHI789" },
                } },
                new Customer{Id = Guid.NewGuid(), Name = "Johans Bulk AB", Address = "Balkvägen 12, 222 22 Stockholm", Vehicles = new List<Vehicle>{
                    new Vehicle{Id = "YS2R4X20005388011", RegistrationNumber = "JKL012" },
                    new Vehicle{Id = "YS2R4X20005387949", RegistrationNumber = "MNO345" },
                } },
                new Customer{Id = Guid.NewGuid(), Name = "Haralds Värdetransporter AB", Address = "Budgetvägen 1, 333 33 Uppsala", Vehicles = new List<Vehicle>{
                    new Vehicle{Id = "YS2R4X20005387765", RegistrationNumber = "PQR678" },
                    new Vehicle{Id = "YS2R4X20005387055", RegistrationNumber = "STU901" },
                } }
            };
        }

        public DefaultVehiclesCreator(MonitorVDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateVehicles();
        }

        private void CreateVehicles()
        {
            foreach (var customer in InitialCustomers)
            {
                AddCustomerIfNotExists(customer);
            }
        }

        private void AddCustomerIfNotExists(Customer customer)
        {
            if (_context.Customers.IgnoreQueryFilters().Any(l => l.Name == customer.Name))
            {
                return;
            }

            _context.Customers.Add(customer);
            _context.SaveChanges();
        }
    }
}
