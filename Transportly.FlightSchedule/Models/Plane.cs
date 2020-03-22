namespace Transportly.FlightSchedule.Models
{
    public class Plane
    {
        public string Id { get; set; }
        public int? TakeOffDay { get; set; }
        public int Capacity { get; set; }
        public int Occupied { get; set; }
        public bool IsAvailable => (Occupied < Capacity);

        public Plane(string id, int capacity)
        {
            Id = id;
            Capacity = capacity;
            Occupied = 0;
            TakeOffDay = null;
        }

        public void GetPlace()
        {
            if (IsAvailable)
            {
                Occupied++;
            }
        }

        public void SetTakeOffDay(int takeOffDay)
        {
            TakeOffDay = takeOffDay;
        }
    }
}
