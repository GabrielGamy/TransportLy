using System;

namespace Transportly.FlightSchedule.Models
{
    public class Flight
    {
        private Plane Plane { get; set; }
        public bool HasPlane => Plane != null;
        public bool IsAvailable => (HasPlane && Plane.IsAvailable);
        public Airport Departure { get; set; }
        public Airport Arrival { get; set; }

        public Flight(Airport departure, Airport arrival)
        {
            Departure = departure;
            Arrival = arrival;
        }

        public void Schedule(int flightNumber)
        {
            if (Plane != null && Plane.TakeOffDay.HasValue)
            {
                Console.WriteLine($"Flight: {flightNumber}, Departure: {Departure}, Arrival: {Arrival}, Day: {Plane.TakeOffDay.Value}, Plane: {Plane.Id}");
            }
        }

        public void Schedule(string orderId, int flightNumber)
        {
            if (Plane != null && Plane.TakeOffDay.HasValue)
            {
                Console.WriteLine($"Order: {orderId}, Flight: {flightNumber}, Departure: {Departure}, Arrival: {Arrival}, Day: {Plane.TakeOffDay.Value}, Plane: {Plane.Id}");
            }
        }

        public void SetPlane(Plane plane, int takeOffDay)
        {
            plane.SetTakeOffDay(takeOffDay);
            Plane = plane;
        }

        public void GetPlace()
        {
            Plane.GetPlace();
        }
    }
}
