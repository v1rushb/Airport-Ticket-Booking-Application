using Airport.Models;
using Airport.Enums;

namespace Airport.Service {
    public class BookingService(FlightService flightService)
    {
        private readonly List<Booking> _bookings = []; // TODO check why var here is not noice
        private readonly FlightService _flightService = flightService;

        public FlightService FlightService {
            get {
                return _flightService;
            }
        }

        public void Book(Flight targetFlight, Passenger booker) {
            _bookings.Add(new Booking { 
                BID = Guid.NewGuid().ToString(),
                flight = targetFlight,
                passenger = booker,
                BookingDate = DateTime.Now,
                Price = targetFlight.Cost * GetPriceMultiplier(targetFlight.Class),
            });
            System.Console.WriteLine("Booked successfully!"); //remove later.
        }

        public List<Booking> GetAllBookings() {
            return _bookings;
        }

        public Booking? GetBookingByID(string ID) {
            return _bookings.FirstOrDefault(el => el.BID == ID);
        }
        public static int GetPriceMultiplier(FlightClass flightClass) { // TODO: make a hashmap implemenation l8r
            int ans = 1;
            switch (flightClass)
            {
                case FlightClass.Economy:
                    ans = 1;
                    break;
                case FlightClass.Business:
                    ans = 3;
                    break;
                case FlightClass.FirstClass:
                    ans = 5;
                    break;
            }
            return ans;
        }

        public bool CancelBooking(string ID) {
            var booking = GetBookingByID(ID);
            if(booking != null) {
                _bookings.Remove(booking);
                Console.WriteLine("Booking's been removed.");
                return true;
            }
            Console.WriteLine("Booking wasn't found.");
            return false;
        }

        public bool ModifyBooking(string ID, Flight newFlight) {
            var booking = GetBookingByID(ID);
            var targetFlight = _flightService.GetFlightByID(newFlight.FlightID);
            if(targetFlight == null) { // probably too specific?
                Console.WriteLine("Flight does not exist.");
                return false;
            }
            if(booking != null) {
                booking.flight = newFlight;
                booking.Price = newFlight.Cost * GetPriceMultiplier(newFlight.Class);
                Console.WriteLine($"Booking's been modified. current book: {booking}");
                return true;
            }
            Console.WriteLine("Booking not found.");
            return false;
        }
    }
}