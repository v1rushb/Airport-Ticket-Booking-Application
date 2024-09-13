using System.Data;
using Airport.Interfaces;
using Airport.Models;

namespace Airport.UserInterface {
    public class Manager {
        private readonly IFlightService _flightService;
        private readonly IBookingService _bookingService;
        public Manager(IFlightService flightService, IBookingService bookingService) {
            _flightService = flightService;
            _bookingService = bookingService;
        }

        public void StartMenu() {
            while(true) {
                Console.WriteLine("\nManager Menu:");
                Console.WriteLine("1. Filter Bookings");
                Console.WriteLine("2. Load Flights");
                Console.WriteLine("3. View Validation Rules");
                Console.WriteLine("4. Display All Flights");
                Console.WriteLine("5. Exit");

                var choice = Console.ReadLine();
                switch(choice) {
                    case "1":
                        FilterBookings();
                        break;
                    case "2":
                        LoadFlights();
                        break;
                    case "3":
                        ViewValidationRules();
                        break;
                    case "4":
                        DisplayAllFlights();
                        break;
                    case "5":
                        return;
                    default:
                        System.Console.WriteLine("Invalid Option.");
                        break;
                }
            }
        }

        private void FilterBookings() {
            var bookings = _bookingService.GetAllBookings();
    
            if (bookings == null || !bookings.Any()) {
                Console.WriteLine("No bookings available.");
                return;
            }

            foreach (var booking in bookings) {
                booking.DisplayBookingInfo();
            }
        }

        private void LoadFlights() {
            System.Console.WriteLine("Enter the path of the target CSV file: ");
            var targetCSVFilePath = Console.ReadLine();
            _flightService.LoadFlights(targetCSVFilePath); //again. do the validations

            // TODO: do some additional logic here later
        }

        private void ViewValidationRules() {
            var rules = _flightService.GetValidatedRules();
            foreach (var rule in rules)
            {
                Console.WriteLine($"{rule.Key}: {rule.Value}");
            }
        }

        private void DisplayAllFlights() {
            var flights = _flightService.LookUpFlights(new SearchCriteria{});
            foreach(var flight in flights) {
                flight.DisplayFlightInfo();
            }
        }
    }
}