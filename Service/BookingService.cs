using Airport.Models;
using Airport.Enums;
using Airport.Interfaces;

namespace Airport.Service {
    public class BookingService : IBookingService {
        private readonly IRepository<Booking> _bookingRepository;
        private readonly IFlightService _flightService;
        private readonly IValidationService _validateService;

        public BookingService(IRepository<Booking> bookingRepository, IFlightService flightService, IValidationService validationService) {
            _bookingRepository = bookingRepository;
            _flightService = flightService;
            _validateService = validationService;
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
    }
}