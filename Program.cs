using Airport.Interfaces;
using Airport.Models;
using Airport.Repositories;
using Airport.Service;
using Airport.Utilties;

namespace Airport {
    class Program {
        internal static void Main() {
            var flightService = new FlightService(new FlightRepository(), new LoadFlightsFromCSV(), new SaveFlightsIntoCSV(), new ValidationService());
            var bookingService = new BookingService(new BookingRepository(), flightService, new ValidationService());
            
        }
    }
}