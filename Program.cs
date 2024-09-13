using Airport.Interfaces;
using Airport.Models;
using Airport.Repositories;
using Airport.Service;
using Airport.UserInterface;
using Airport.Utilties;

namespace Airport {
    class Program {
        internal static void Main() {
            var flightRepository = new FlightRepository();
            var bookingRepository = new BookingRepository();
            var validationService = new ValidationService();
            var csvFlightLoader = new LoadFlightsFromCSV();
            var csvFlightSaver = new SaveFlightsIntoCSV();
            var csvPassenger = new LoadPassengersFromCSV();
            var passengerRepository = new PassengerRepository(csvPassenger);
            var csvBookingLoader = new LoadBookingsFromCSV(flightRepository, passengerRepository);

            IFlightService flightService = new FlightService(flightRepository, csvFlightLoader, csvFlightSaver, validationService);
            IBookingService bookingService = new BookingService(bookingRepository, flightService, validationService, csvBookingLoader);

            var app = new BookingApplication(flightService, bookingService, passengerRepository);
            app.Start();
        }
    }
}