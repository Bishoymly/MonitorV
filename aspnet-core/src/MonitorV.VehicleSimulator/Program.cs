using Abp;
using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Threading;

namespace MonitorV.Vehicle
{
    class Program
    {
        private static Vehicle[] vehicles;
        private static string baseUrl;

        static void Main(string[] args)
        {
            Console.WriteLine("Starting..");

            IConfiguration config = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json", false, true)
              .Build();

            baseUrl = config["App:ApiBaseUrl"];

            HttpResponseMessage result = null;
            do
            {
                // Get list of vehicles from the API
                var client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Add("accept", "application/json");

                try
                {
                    result = client.GetAsync("api/services/app/Vehicles/GetAll").Result;
                    Console.WriteLine($"Status:{result.StatusCode}");
                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var getAllResult = JsonConvert.DeserializeObject<GetAllResult>(result.Content.ReadAsStringAsync().Result);
                        vehicles = getAllResult.result.items;

                        // Start a timer to simulate vehicles connections
                        Console.WriteLine($"Simulating {getAllResult.result.totalCount} vehicles..");
                        var t = new Timer(TimerCallback, null, 0, 15000);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Thread.Sleep(10000);
                }                
            }
            while (result?.StatusCode != System.Net.HttpStatusCode.OK);

            // Wait too much before closing this console
            Thread.Sleep(3600000);
        }

        private static void TimerCallback(Object o)
        {
            // Pick a random vehicle
            var r = new Random();
            var i = r.Next(vehicles.Length);
            var vehicle = vehicles[i];

            // Update the status of this vehicle
            Console.WriteLine($"Vehicle {vehicle.id} is connecting");

            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Add("accept", "application/json");
            var result = client.PutAsync("api/services/app/Vehicles/UpdateStatus?id=" + vehicle.id, new StringContent("")).Result;
            result.EnsureSuccessStatusCode();
        }
    }
}
