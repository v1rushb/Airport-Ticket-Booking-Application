using Airport.Models;

namespace Airport.Utilties {
    public interface ICSVBookingLoader {
        List<Booking> LoadBookings(string targetCSVFilePath); 
    }
}