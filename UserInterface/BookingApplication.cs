using Airport.Interfaces;

namespace Airport.UserInterface {
    public class BookingApplication {
        private readonly IFlightService _flightService;
        private readonly IBookingService _bookingService;

        public BookingApplication(IFlightService flightService, IBookingService bookingService {
            _flightService = flightService;
            _bookingService = bookingService;
        }

        public void Start() {
            System.Console.WriteLine("Welcome to the Airport System!");
            System.Console.WriteLine("Are you a Passenger or a Manager? (P/M)");
            var choice = Console.ReadLine(); // probs read?

            if(choice.Equals("P", StringComparison.OrdinalIgnoreCase)) {
                var passengerUI = new Airport.UserInterface.Passenger(_flightService, _bookingService);
                passengerUI.StartMenu();
            } else if (choice.Equals("M", StringComparison.OrdinalIgnoreCase)) {
                // manager now
            } else {
                System.Console.WriteLine("Invalid option.");
            }
        }
    }
}