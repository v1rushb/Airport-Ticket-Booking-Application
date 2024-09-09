using System;

namespace Airport {
    public class Flight {
        int FlightID { get; set; } // probs not practical for large datasets? TODO -> look for an alternative way.
        int Cost { get; set; } 

        string DepartedCountry { get; set; }
        string DestinatedCountry { get; set; }

        DateTime DepartureTime { get; set; }
    }
}