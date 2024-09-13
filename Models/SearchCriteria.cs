using Airport.Enums;

namespace Airport.Models {
    public class SearchCriteria {
        public decimal? Cost { get; set; }
        public string? DepartedCountry { get; set;}

        public string? DestinatedCountry { get; set; }

        public DateTime? DepartureTime { get; set; }

        public string? ArrivalAirport { get; set; }
        public string? DepartedAirport { get; set; }

        public FlightClass? ClassType { get; set; }
    }
}