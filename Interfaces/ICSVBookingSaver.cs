using Airport.Models;

namespace Airport.Interfaces {
    public interface ICSVBookingSaver {
        void SaveBookings(string path, List<Booking> bookings); 
    }
}