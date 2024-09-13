using System;
using Airport.Enums;
using Airport.Models;

namespace Airport.Interfaces {
    public interface IBookingService {
        Booking BookFlight(Passenger passenger, Flight flight);
        void CancelBooking(string BID);
        void ModifyBooking(Booking booking);
        IEnumerable<Booking> GetBookingByPassenger(string passengerID);
        IEnumerable<Booking> GetAllBookings();
    }
}