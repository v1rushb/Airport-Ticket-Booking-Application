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

            IFlightService flightService = new FlightService(flightRepository, csvFlightLoader, csvFlightSaver, validationService);
            IBookingService bookingService = new BookingService(bookingRepository, flightService, validationService);

            var app = new BookingApplication(flightService, bookingService);
            app.Start();
        }
    }
}