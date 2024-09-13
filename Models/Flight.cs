using Airport.Enums;
using Airport.Utilties;
using System.ComponentModel.DataAnnotations;

namespace Airport.Models {
    public class Flight {   
        [Required]
        public string FlightID { get; set; } // probs not practical for large datasets? TODO -> look for an alternative way.
        // check booking TODO
        
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be positive.")]
        public decimal Cost { get; set; } 

        [Required]
        public string DepartedCountry { get; set; }
        [Required]
        public string DestinatedCountry { get; set; }

        [Required]
        [FutureDate(ErrorMessage = "Departure Time must be in the future.")]
        public DateTime DepartureTime { get; set; }

        [Required]
        public FlightClass Class { get; set; }

        [Required]
        public string ArrivalAirport { get; set; }

        [Required]
        public string DepartedAirport { get; set; } 



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

        public override string ToString() => $"{FlightID}, {Cost}";
    }
}