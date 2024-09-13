namespace Airport.Models {
    public class Booking {
        public required string BID { get; set; } // TODO lookup other things to make an int ID. 
        public required Flight flight { get; set; }
        public required Passenger passenger { get; set; }
        public required DateTime BookingDate { get; set; }

        public required double Price { get; set; }

        public void DisplayBookingInfo()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"Booking ID: {BID}");
            sb.AppendLine($"Passenger: {passenger.Name}, Age: {passenger.Age}, Phone: {passenger.Phone}");
            sb.AppendLine($"Flight ID: {flight.FlightID}, Class: {flight.Class}");
            sb.AppendLine($"Price: ${Price}, Booking Date: {BookingDate}");
            Console.WriteLine(sb.ToString());
        }
    }
}