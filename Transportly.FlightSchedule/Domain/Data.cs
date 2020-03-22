using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Transportly.FlightSchedule.Models;

namespace Transportly.FlightSchedule.Domain
{
    public class Data
    {
        public static List<Order> GetAllOrders()
        {
            // Missing orders (order-058, order-059, order-057, order-079)
            var ordersJson = File.ReadAllText("coding-assigment-orders.json");

            return (JsonConvert.DeserializeObject<Dictionary<string, Order>>(ordersJson)
                .Select(item =>
                {
                    item.Value.Id = item.Key;
                    return item.Value;
                }).ToList());
        }

        public static List<Flight> GetFlights()
        {
            var Montreal = new Airport("YUL", "Montreal");
            var Toronto = new Airport("YYZ", "Toronto");
            var Calgary = new Airport("YYC", "Calgary");
            var Vancouver = new Airport("YVR", "Vancouver");

            return new List<Flight>()
            {
                new Flight(Montreal, Toronto),
                new Flight(Montreal, Calgary),
                new Flight(Montreal, Vancouver)
            };
        }

        public static List<Plane> GetPlanes(int numberOfPlanes, int maxCapacityByPlane)
        {
            List<Plane> planes = new List<Plane>();
            for (var i = 0; i < numberOfPlanes; i++)
            {
                planes.Add(new Plane($"Plane-00{i + 1}", maxCapacityByPlane));
            }
            return planes;
        }
    }
}
