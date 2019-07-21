using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorV.Vehicle
{

    public class GetAllResult
    {
        public Result result { get; set; }
        public object targetUrl { get; set; }
        public bool success { get; set; }
        public object error { get; set; }
        public bool unAuthorizedRequest { get; set; }
        public bool __abp { get; set; }
    }

    public class Result
    {
        public int totalCount { get; set; }
        public Vehicle[] items { get; set; }
    }

    public class Vehicle
    {
        public string registrationNumber { get; set; }
        public string customerName { get; set; }
        public string customerAddress { get; set; }
        public DateTime? lastStatusUpdate { get; set; }
        public string id { get; set; }
    }

}
