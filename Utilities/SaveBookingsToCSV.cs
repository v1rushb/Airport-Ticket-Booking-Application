using Airport.Models;
using Airport.Interfaces;

namespace Airport.Utilties {
    public class SaveBookingIntoCSV : ICSVBookingSaver {
        public void SaveBookings(string path, List<Booking> bookings) {
            try {
                    using (var writer = new StreamWriter(path)) {
                    foreach (var booking in bookings) {
                        writer.WriteLine($"{booking.BID},{booking.flight.FlightID},{booking.passenger.ID}," +
                                         $"{booking.BookingDate},{booking.Price},{booking.FlightClassType}");
                    }
                }
                Console.WriteLine("Bookings saved successfully.");
            } catch(IOException ex) {
                throw new ApplicationException($"An error occurred while writing to the file: {path}", ex);
            } catch(Exception ex) {
                Console.WriteLine($"Something's up with file: {path}");
                Console.WriteLine($"More info: {ex}");
                throw;
            }
        }
    }
}