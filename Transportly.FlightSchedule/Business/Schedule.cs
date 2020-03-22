using System;
using System.Collections.Generic;
using System.Linq;
using Transportly.FlightSchedule.Domain;
using Transportly.FlightSchedule.Models;

namespace Transportly.FlightSchedule.Business
{
    public class Schedule
    {
        public FlightScheduleOptions FlightScheduleOptions { get; set; }
        public List<Flight> AvalaibleFlights { get; set; }
        public List<Plane> AvalaiblePlanes { get; set; }
        public List<Order> AvalaibleOrders { get; set; }
        public Schedule(FlightScheduleOptions options)
        {
            FlightScheduleOptions = options;
        }

        public void ScheduleWithoutOrders()
        {
            InitializeData();

            Console.WriteLine("###### USER STORY #1 ######");

            var flightNumber = 0;
            var flights = new Queue<Flight>(AvalaibleFlights);
            var planes = new Queue<Plane>(AvalaiblePlanes);

            if (flights.Count == 0 || planes.Count == 0)
            {
                return;
            }

            for (var i = 0; i < FlightScheduleOptions.DaysScheduled; i++)
            {
                int currentDay = (i + 1);
                Console.WriteLine($"Day {currentDay}");

                if (!flights.Any())
                {
                    // All flights in the queue have been scheduled.
                    flights = new Queue<Flight>(AvalaibleFlights);
                }

                if (!planes.Any())
                {
                    // All planes have been scheduled for the current day.
                    planes = new Queue<Plane>(AvalaiblePlanes);
                }

                while (flights.Any() && planes.Any())
                {
                    var currentFlight = flights.Dequeue();
                    currentFlight.SetPlane(planes.Dequeue(), currentDay);
                    currentFlight.Schedule(++flightNumber);
                }
            }

            Console.WriteLine("###### END - USER STORY #1 ######");
        }

        public void ScheduleWithOrders()
        {
            InitializeData();

            Console.WriteLine("###### USER STORY #2 ######");

            var flightNumber = 0;
            var flights = new Queue<Flight>(AvalaibleFlights);
            var planes = new Queue<Plane>(AvalaiblePlanes);
            var scheduled = new List<Order>();

            if (flights.Count == 0 || planes.Count == 0)
            {
                return;
            }

            for (var i = 0; i < FlightScheduleOptions.DaysScheduled; i++)
            {
                int currentDay = (i + 1);
                Console.WriteLine($"Day {currentDay}");

                if (!flights.Any())
                {
                    // All flights in the queue have been scheduled.
                    flights = new Queue<Flight>(AvalaibleFlights);
                }

                if (!planes.Any())
                {
                    // All planes have been scheduled for the current day.
                    planes = new Queue<Plane>(AvalaiblePlanes);
                }

                var orders = new Queue<Order>(AvalaibleOrders.Where(o => !scheduled.Contains(o)));

                while (orders.Any())
                {
                    var nextOrder = orders.Dequeue();

                    while (flights.Any() && planes.Any())
                    {
                        var flight = flights
                            .Where(f => f.Arrival.Code.Equals(nextOrder.Destination))
                            .FirstOrDefault();

                        if (flight == null)
                        {
                            break;
                        }

                        if (!flight.HasPlane)
                        {
                            flight.SetPlane(planes.Dequeue(), currentDay);
                        }

                        if (flight.IsAvailable)
                        {
                            flight.GetPlace();
                            flight.Schedule(nextOrder.Id, ++flightNumber);

                            scheduled.Add(nextOrder);
                            break;
                        }
                        else
                        {
                            // The flight is full of boxes.
                            flights.Dequeue();
                        }
                    }
                }
            }

            DisplayNotScheduledOrders(scheduled);

            Console.WriteLine("###### END - USER STORY #2 ######");
        }

        private void DisplayNotScheduledOrders(List<Order> orders)
        {
            if (orders.Any())
            {
                Console.WriteLine("###### NOT SCHEDULED ######");

                foreach (var order in orders)
                {
                    Console.WriteLine($"Order: {order.Id}, Flight: not scheduled");
                }

                Console.WriteLine("###### END NOT SCHEDULED ######");
            }
        }

        private void InitializeData()
        {
            AvalaibleFlights = Data.GetFlights();
            AvalaiblePlanes = Data.GetPlanes(
                FlightScheduleOptions.NumberOfPlanes,
                FlightScheduleOptions.MaxCapacityByPlane);
            AvalaibleOrders = Data.GetAllOrders();
        }
    }
}
