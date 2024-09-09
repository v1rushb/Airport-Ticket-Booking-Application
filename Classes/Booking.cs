using System;

namespace Airport {
    public class Booking {
        public int BID { get; set; }
        public Flight flight { get; set; }
        public Passenger passenger { get; set; }
        DateTime BookingDate { get; set; }
    }
}