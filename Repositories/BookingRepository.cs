using System;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using Airport.Interfaces;
using Airport.Models;

namespace Airport.Repositories {
    public class BookingRepository : IRepository<Booking> {
        private readonly string targetCSVFilePath = "Data/Bookings.csv";
        private List<Booking> _bookings;

        public IEnumerable<Booking> GetAll() {
            return _bookings;
        }

        public Booking? GetByID(string ID) {
            try {
                return _bookings.SingleOrDefault(el => el.BID.Equals(ID));
            } catch(Exception ex) {
                System.Console.WriteLine($"Error While fetching a booking with the ID: {ID} \n more  info: {ex}"); // idk handle using CEH?
                throw new ApplicationException("Error While fetching a booking with the ID: {ID}", ex);
            }
        }

        public void Add(Booking booking) {
            var currentBooking = GetByID(booking.BID);
            if(currentBooking == null) {
                _bookings.Add(booking);
                return;
            }
            throw new DuplicateNameException("Booking Already Exists.");
        }

        public void Update(Booking booking) {
            var currentBooking = GetByID(booking.BID);
            if(currentBooking != null) {
                _bookings.Remove(booking);
                _bookings.Add(booking);
            }
        }

        public void Delete(string ID) {
            var currentBooking = GetByID(ID);
            if(currentBooking != null) {
                _bookings.Remove(currentBooking);
            }
        }
    }
}