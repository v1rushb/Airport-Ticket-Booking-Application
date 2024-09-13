using System.Runtime.CompilerServices;
using Airport.Interfaces;
using Airport.Repositories;

namespace Airport.UserInterface {
    public class BookingApplication {
        private readonly IFlightService _flightService;
        private readonly IBookingService _bookingService;
        private readonly PassengerRepository _passengerRepository;

        public BookingApplication(IFlightService flightService, IBookingService bookingService, PassengerRepository passengerRepository) {
            _flightService = flightService;
            _bookingService = bookingService;
            _passengerRepository = passengerRepository;
        }

        public void Start() {
            _passengerRepository.LoadAllPassengers();
            _flightService.LoadFlights("Data/Flights.csv");
            _bookingService.LoadBookings("Data/Bookings.csv");
            System.Console.WriteLine("Welcome to the Airport System!");
            System.Console.WriteLine("Are you a Passenger or a Manager? (P/M)");
            var choice = Console.ReadLine(); // probs read?

            if(choice.Equals("P", StringComparison.OrdinalIgnoreCase)) {
                var passengerUI = new Airport.UserInterface.Passenger(_flightService, _bookingService);
                passengerUI.StartMenu();
            } else if (choice.Equals("M", StringComparison.OrdinalIgnoreCase)) {
                var managerUI = new Manager(_flightService, _bookingService);
                managerUI.StartMenu();
            } else {
                System.Console.WriteLine("Invalid option.");
            }
        }
    }
}