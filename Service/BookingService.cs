using System;
using System.Collections;
using System.Collections.Generic;

namespace Airport {
    public class BookingService {
        private readonly List<Booking> _bookings = new List<Booking>(); // TODO check why var here is not noice

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

        public int GetPriceMultiplier(FlightClass flightClass) {
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
    }
}