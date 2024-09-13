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

        public required string ArrivalAirport { get; set; }

        public required string DepartedAirport { get; set; } 



        public void DisplayFlightInfo()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"Flight ID: {FlightID}");
            sb.AppendLine($"Flight Cost: {Cost}");
            sb.AppendLine($"From {DepartedCountry} To {DestinatedCountry}");
            sb.AppendLine($"Departure Time: {DepartureTime} \n");
            sb.AppendLine($"Airrival Airport: {ArrivalAirport} \n");
            sb.AppendLine($"Departed Airport: {DepartedAirport} \n");
            Console.WriteLine(sb.ToString());
        }
    }
}