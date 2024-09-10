using System;

namespace Airport {
    public class Flight {
        public required string FlightID { get; set; } // probs not practical for large datasets? TODO -> look for an alternative way.
        // check booking TODO
        public required int Cost { get; set; } 

        public required string DepartedCountry { get; set; }
        public required string DestinatedCountry { get; set; }

        public required DateTime DepartureTime { get; set; }

        public required FlightClass Class { get; set; }
    }

    public enum FlightClass {
        Economy,
        Business,
        FirstClass,
    }
}