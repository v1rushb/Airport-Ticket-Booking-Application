using System;
using Airport.Enums;
using Airport.Models;

namespace Airport.Interfaces {
    public interface IBookingService {
        Booking? BookFlight(Passenger passenger, Flight flight, FlightClass flightClass);
        void CancelBooking(string BID);
        void ModifyBooking(Booking booking);
        IEnumerable<Booking> GetBookingByPassenger(string passengerID);
        IEnumerable<Booking>? GetAllBookings();

        void LoadBookings(string targetCSVFilePath);
        void SaveBookings(string targetCSVFilePath);
    }
}