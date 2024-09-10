using System;

namespace Airport {
    public abstract class BookingFactory {
        public abstract Booking Book(Flight targetFlight, Passenger booker);
    }
}