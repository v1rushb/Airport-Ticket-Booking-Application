using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Airport.Enums;
using Airport.Interfaces;
using Airport.Models;
using Microsoft.VisualBasic;

namespace Airport.UserInterface {
    public class Passenger {
        private readonly IFlightService _flightService;
        private readonly IBookingService _bookingService;
        private Airport.Models.Passenger currentPassenger;

        public Passenger(IFlightService flightService, IBookingService bookingService) {
            _flightService = flightService;
            _bookingService = bookingService;
        }

        public void StartMenu() {
            System.Console.WriteLine("Please Enter your ID: "); // do validations later
            var passengerID = Console.ReadLine();

            System.Console.WriteLine("Please enter your Name: ");
            var passengerName = Console.ReadLine();

            System.Console.WriteLine("Please enter your Email: ");
            var passengerEmail = Console.ReadLine();

            System.Console.WriteLine("Please enter your age: ");

            var age = Console.ReadLine(); // do validations!

            System.Console.WriteLine("Please Enter your phone Number: ");
            
            var phoneNumber = Console.ReadLine();

            currentPassenger = new Airport.Models.Passenger{
                ID = passengerID,
                Name = passengerName,
                Age = int.Parse(age),
                Email = passengerEmail,
                Phone = phoneNumber,
            };

            while(true) {
                Console.WriteLine("\nPassenger Menu:");
                Console.WriteLine("1. Search Flights");
                Console.WriteLine("2. Book Flight");
                Console.WriteLine("3. View Bookings");
                Console.WriteLine("4. Cancel Booking");
                Console.WriteLine("5. Modify Booking");
                Console.WriteLine("6. View All Flights");
                Console.WriteLine("7. Exit");

                var choice = Console.ReadLine();

                switch(choice) {
                    case "1":
                        SearchFlights();
                        break;
                    case "2":
                        BookFlight();
                        break;
                    case "3":
                        ViewBookings();
                        break;
                    case "4":
                        CancelBooking();
                        break;
                    case "5":
                        ModifyBooking(); //fix me pls
                        break;
                    case "6":

                    default:
                        System.Console.WriteLine("Invalid Option.");
                        break;
                }
            }
            
        }

        private void SearchFlights() {
            /*
                    public decimal? Cost { get; set; }
                public string? DepartedCountry { get; set;}

                public string? DestinatedCountry { get; set; }

                public DateTime? DepartureTime { get; set; }

                public string? ArrivalAirport { get; set; }
                public string? DepartedAirport { get; set; }

                public FlightClass? ClassType { get; set; }
            */

            Console.WriteLine(@"What's the cost? Enter or Ignore 'Press Enter'");
            var cost = Console.ReadLine(); // DO PARSING PLS

            Console.WriteLine(@"What's the departed Country? Enter or Ignore 'Press Enter'");
            var departedCountry = Console.ReadLine();

            Console.WriteLine(@"What's the Destinated Country? Enter or Ignore 'Press Enter'");
            var destinatedCountry = Console.ReadLine();

            Console.WriteLine(@"What's the Departure time? Enter or Ignore 'Press Enter'");
            var departureTime = Console.ReadLine();

            Console.WriteLine(@"What's the Arrival Airport? Enter or Ignore 'Press Enter'");
            var arrivalAirport = Console.ReadLine();

            Console.WriteLine(@"What's the Departed Airport? Enter or Ignore 'Press Enter'");
            var departedAirport = Console.ReadLine();

            Console.WriteLine(@"What's the Flight Class? Enter or Ignore 'Press Enter'");
            var flightClass = Console.ReadLine();


            var criteria = new SearchCriteria{
                Cost = decimal.Parse(cost),
                DepartedCountry = departedCountry,
                DestinatedCountry = destinatedCountry,
                DepartureTime = DateTime.Now, // for now until u do ur shitty validations
                DepartedAirport = departedAirport,
                ArrivalAirport = arrivalAirport,
                ClassType = FlightClass.Business // do when when doing all parsing thingys altogether.
            };

            var flights = _flightService.LookUpFlights(criteria);

            foreach(var flight in flights) {
                System.Console.WriteLine($"Flight Number: {flight.FlightID}, Price: {flight.Cost}");
            }
        }

        private void BookFlight() {
            System.Console.WriteLine("Enter Flight ID to book");
            var id = Console.ReadLine();
            var flight = _flightService.LookUpFlights(new SearchCriteria { }).FirstOrDefault(el => el.FlightID.Equals(id));
            if(flight != null) {
                System.Console.WriteLine("Select Class: {Economy / Business / First Class}");
                var classChoice = Console.ReadLine();
                if(Enum.TryParse(classChoice, out FlightClass flightClass)) {
                    var newBooking = _bookingService.BookFlight(currentPassenger, flight); // ughhh i need to pass classtype as well

                    if(newBooking != null) {
                        System.Console.WriteLine($"Noice, you've booked! \n Info: {newBooking}");
                    } else {
                        System.Console.WriteLine("Booking failed.");
                    }
                } else {
                    System.Console.WriteLine("Invalid Class Specification.");
                }
            } else {
                System.Console.WriteLine("Flight not found!");
            }
        }

        private void ViewBookings() {
            var bookings = _bookingService.GetBookingByPassenger(currentPassenger.ID);
            if(bookings != null) {
                System.Console.WriteLine("Here are your Bookings: ");
                foreach(var booking in bookings) {
                    System.Console.WriteLine(booking);
                }
            } else {
                System.Console.WriteLine("No bookings found.");
            }
        }

        private void CancelBooking() {
            System.Console.WriteLine("Enter Booking ID to cancel: ");
            var choice = Console.ReadLine();
            var existingBooking = _bookingService.GetBookingByPassenger(choice);
            if(existingBooking != null) {
                _bookingService.CancelBooking(choice);
                return;
            }
            System.Console.WriteLine("No booking was found.");
        }

        private void ModifyBooking() {
            //TODO: do later after fixing class type in booking and flight class
        }

        private void GetAllFlights() {
            var flights = _flightService.LookUpFlights(new SearchCriteria { });
            if(flights.Count() != 0) {
                System.Console.WriteLine("Displaying Current Available flights:"); // fix date thingy later.
                foreach(var flight in flights) {
                    System.Console.WriteLine(flight);
                }
            }
        }
    }
}