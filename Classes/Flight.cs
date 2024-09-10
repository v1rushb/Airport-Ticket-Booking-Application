using System;

namespace Airport {
    public class Flight {
        public string FlightID { get; set; } // probs not practical for large datasets? TODO -> look for an alternative way.
        // check booking TODO
        public int Cost { get; set; } 

        public string DepartedCountry { get; set; }
        public string DestinatedCountry { get; set; }

        public DateTime DepartureTime { get; set; }

        public FlightClass Class { get; set; }
    }

    public enum FlightClass {
        Economy,
        Business,
        FirstClass,
    }
}