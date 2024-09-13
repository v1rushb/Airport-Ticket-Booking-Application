using Airport.Models;
using Airport.Enums;
using Airport.Interfaces;
using Airport.Utilties;

namespace Airport.Service {
    public class BookingService : IBookingService {
        private readonly IRepository<Booking> _bookingRepository;
        private readonly IFlightService _flightService;
        private readonly IValidationService _validateService;
        private readonly ICSVBookingLoader _csvBookLoader;

        public BookingService(IRepository<Booking> bookingRepository, IFlightService flightService, IValidationService validationService, ICSVBookingLoader csvBookLoader) {
            _bookingRepository = bookingRepository;
            _flightService = flightService;
            _validateService = validationService;
            _csvBookLoader = csvBookLoader;
        }

        public Booking? BookFlight(Passenger bpassenger, Flight targetFlight) {
            var newBooking = new Booking {
                BID = Guid.NewGuid().ToString(),
                passenger = bpassenger,
                flight = targetFlight,
                BookingDate = DateTime.Now,
                Price = targetFlight.Cost * GetPriceMultiplier(targetFlight.Class)
            };

            var validationResult = _validateService.Validate(newBooking);
            if(validationResult.IsValid) {
                _bookingRepository.Add(newBooking);
                return newBooking;
            }
            //check errors here.
            return null;
        }

        public int GetPriceMultiplier(FlightClass flightClass) { // TODO: make a hashmap implemenation l8r
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

        public void CancelBooking(string BID) {
            _bookingRepository.Delete(BID);
        }

        public void ModifyBooking(Booking booking) {
            _bookingRepository.Update(booking);
        }

        public IEnumerable<Booking> GetBookingByPassenger(string passengerID) {
            return _bookingRepository.GetAll().Where(el => el.passenger.ID.Equals(passengerID)).ToList();
        }

        public IEnumerable<Booking> GetAllBookings() {
            return _bookingRepository.GetAll();
        }

        public void LoadBookings(string targetCSVFilePath) {
            var bookings = _csvBookLoader.LoadBookings(targetCSVFilePath);
            var validBookings = new List<Booking>();
            var errors = new Dictionary<Booking, List<string>>();

            foreach (var booking in bookings) {
                var validationResult = _validateService.Validate(booking);
                if (validationResult.IsValid) {
                    validBookings.Add(booking);
                } else {
                    errors.Add(booking, validationResult.Errors.Select(e => e.ErrorMessage).ToList());
                }
            }

            foreach (var booking in validBookings) {
                System.Console.WriteLine(booking);
                _bookingRepository.Add(booking);
            }

            if (errors.Any()) {
                foreach (var error in errors) {
                    System.Console.WriteLine($"Errors for booking {error.Key.BID}: ");
                    foreach (var msg in error.Value) {
                        System.Console.WriteLine($"- {msg}");
                    }
                }
            }
        }

        void IBookingService.SaveBookings(string targetCSVFilePath)
        {
            throw new NotImplementedException();
        }
    }
}