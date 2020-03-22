using Microsoft.Extensions.Configuration;
using System.IO;
using Transportly.FlightSchedule.Business;

namespace Transportly.FlightSchedule
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration appConfig = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var flightScheduleOptions = new FlightScheduleOptions();
            appConfig.GetSection("flightSchedule").Bind(flightScheduleOptions);

            var schedule = new Schedule(flightScheduleOptions);
            schedule.ScheduleWithoutOrders();
            schedule.ScheduleWithOrders();
        }
    }
}
