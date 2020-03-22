namespace Transportly.FlightSchedule.Models
{
    public class Airport
    {
        public string Code { get; set; }
        public string City { get; set; }
        public string Name { get; set; }

        public Airport(string code, string city)
        {
            Code = code;
            City = city;
            Name = $"{city} ({code})";
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
