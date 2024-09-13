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
            string passengerName, passengerEmail, phoneNumber;
            int age;
            while (true) {
                System.Console.WriteLine("Please enter your Name: ");
                passengerName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(passengerName)) {
                    break;
                }
                System.Console.WriteLine("Name cannot be empty. Please enter a valid name.");
            }

            while (true) {
                System.Console.WriteLine("Please enter your Email: ");
                passengerEmail = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(passengerEmail)) {
                    break;
                }
                System.Console.WriteLine("Invalid email format. Please enter a valid email.");
            }

            while (true) {
                int lowerBoundAgeLimit = 12, upperBoundAgeLimit = 120;
                System.Console.WriteLine("Please enter your Age: ");
                var ageInput = Console.ReadLine();
                if (int.TryParse(ageInput, out age) && age > lowerBoundAgeLimit && age <= upperBoundAgeLimit) {
                    break;
                }
                System.Console.WriteLine("Invalid age. Please enter a valid age between 12 and 120.");
            }

            while (true) {
                System.Console.WriteLine("Please Enter your Phone Number: ");
                phoneNumber = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(phoneNumber) && phoneNumber.All(char.IsDigit)) { // do palestinian one later.
                    break;
                }
                System.Console.WriteLine("Invalid phone number. Please enter a valid phone number.");
            };

            currentPassenger = new Airport.Models.Passenger{
                ID = Guid.NewGuid().ToString(),
                Name = passengerName,
                Age = age,
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
                        GetAllFlights();
                        break;

                    case "7":
                        return;

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
            var costInput = Console.ReadLine();
            decimal? cost = null;
            if (!string.IsNullOrEmpty(costInput) && decimal.TryParse(costInput, out var parsedCost)) {
                cost = parsedCost;
            }

            Console.WriteLine(@"What's the departed Country? Enter or Ignore 'Press Enter'");
            var departedCountry = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(departedCountry)) {
                departedCountry = null;
            }

            Console.WriteLine(@"What's the Destinated Country? Enter or Ignore 'Press Enter'");
            var destinatedCountry = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(destinatedCountry)) {
                destinatedCountry = null;
            }

            Console.WriteLine(@"What's the Departure time? Enter or Ignore 'Press Enter'");
            var departureTimeInput = Console.ReadLine();
            DateTime? departureTime = null;
            if (!string.IsNullOrEmpty(departureTimeInput) && DateTime.TryParse(departureTimeInput, out var parsedDateTime)) {
                departureTime = parsedDateTime;
            }

            Console.WriteLine(@"What's the Arrival Airport? Enter or Ignore 'Press Enter'");
            var arrivalAirport = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(arrivalAirport)) {
                arrivalAirport = null;
            }

            Console.WriteLine(@"What's the Departed Airport? Enter or Ignore 'Press Enter'");
            var departedAirport = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(departedAirport)) {
                departedAirport = null;
            }

            Console.WriteLine(@"What's the Flight Class? Enter or Ignore 'Press Enter'");
            var flightClassInput = Console.ReadLine();
            FlightClass? flightClass = null;
            if (!string.IsNullOrEmpty(flightClassInput) && Enum.TryParse<FlightClass>(flightClassInput, out var parsedClass)) {
                flightClass = parsedClass;
            }


            var criteria = new SearchCriteria{
                Cost = cost,
                DepartedCountry = departedCountry,
                DestinatedCountry = destinatedCountry,
                DepartureTime = departureTime,
                DepartedAirport = departedAirport,
                ArrivalAirport = arrivalAirport,
                ClassType = flightClass
            };

            var flights = _flightService.LookUpFlights(criteria);

            if(flights.Any()) {
                foreach(var flight in flights) {
                    flight.DisplayFlightInfo();
                }
                return;
            }
            System.Console.WriteLine("No flights Matched your criteria.");
        }

        private void BookFlight() {
            System.Console.WriteLine("Enter Flight ID to book");
            var id = Console.ReadLine();
            var flight = _flightService.LookUpFlights(new SearchCriteria { }).FirstOrDefault(el => el.FlightID.Equals(id));
            if(flight != null) {
                System.Console.WriteLine("Select Class: {Economy / Business / FirstClass}");
                var classChoice = Console.ReadLine();
                if(Enum.TryParse(classChoice, out FlightClass flightClass)) {
                    var newBooking = _bookingService.BookFlight(currentPassenger, flight, flightClass); // ughhh i need to pass classtype as well

                    if(newBooking != null) {
                        System.Console.WriteLine($"Noice, you've booked! \n Info:");
                        newBooking.DisplayBookingInfo();
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
                    booking.DisplayBookingInfo();
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
                System.Console.WriteLine("Booking has been canceled!");
                return;
            }
            System.Console.WriteLine("No booking was found.");
        }

        private void ModifyBooking() {
            //TODO: do later after fixing class type in booking and flight class
            System.Console.WriteLine("Enter Booking ID to modify: ");
            var choice = Console.ReadLine();
            var existingBooking = _bookingService.GetBookingByPassenger(currentPassenger.ID).FirstOrDefault(el => el.BID.Equals(choice));
            if(existingBooking == null) {
                System.Console.WriteLine($"Booking with ID {choice} not found.");
            }

            System.Console.WriteLine(@"Enter a new Flight ID, or just Press 'Enter'.");
            var newFlightID = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(newFlightID)) {
                var newFlight = _flightService.LookUpFlights(new SearchCriteria { }).FirstOrDefault(el => el.FlightID.Equals(newFlightID));
                if(newFlight == null) {
                    System.Console.WriteLine($"{newFlightID} is an invalid ID for a flight.");
                    return;
                }
                existingBooking.flight = newFlight;
            }
            System.Console.WriteLine("Select Class: {Economy / Business / FirstClass}");
            var newClassChoice = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(newClassChoice)) {
                if(Enum.TryParse(newClassChoice, out FlightClass flightClass)) {
                    existingBooking.FlightClassType = flightClass;
                } else {
                    System.Console.WriteLine($"Invalid class type: {newClassChoice}. Please enter a valid class.");
                    return;
                }
            }
                _bookingService.ModifyBooking(existingBooking);
                System.Console.WriteLine("Booking's been changed successfully!");

        }

        private void GetAllFlights() {
            var flights = _flightService.LookUpFlights(new SearchCriteria { });
            if(flights.Count() != 0) {
                System.Console.WriteLine("Displaying Current Available flights:"); // fix date thingy later.
                foreach(var flight in flights) {
                    flight.DisplayFlightInfo();
                }
            }
        }
    }
}